using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : IActorController
{
    [Header("Move Options")]
    public float walkSpeed = 1.7f;
    public float runMulti = 2.0f;
    public float jumpVelocity = 4.0f;
    public float rollVelocity = 3.0f;
    public float rollVelocityThreshold = 7.0f;

    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 deltaPos;
    private Vector3 thrushVec;
    private bool lockPlanar = false;
    private bool trackDirection = false;
    private bool canAttack;

    [Header("Physic")]
    public PhysicMaterial fricitionOne;
    public PhysicMaterial fricitionZero;

    public bool leftIsShield = true;

    public delegate void OnActionDelegate();
    public event OnActionDelegate OnActionPressed;

    private CapsuleCollider col;
    private ActorManager am;
    void Awake()
    {
        am = GetComponent<ActorManager>();
        pi = GetComponent<IPlayerInput>();
        col = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
        camcon = GetComponentInChildren<CameraController>();
        model = transform.GetChild(0).gameObject;
        anim = model.GetComponent<Animator>();
        if (anim == null || rigid == null || camcon == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        LocolMotion();
        Jump();
        Roll();
        Attack();
        Action();

        if (anim.GetFloat("forward") > 1.9f)
            am.StateM.CountVigor(-am.StateM.state.runCost);
    }

    private void LocolMotion()
    {
        if (camcon.lockState)
        {
            Vector3 localDevc = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forward", localDevc.z * ((pi.Run && am.TryDoRun()) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDevc.x * ((pi.Run && am.TryDoRun()) ? 2.0f : 1.0f));

            if (trackDirection)
                model.transform.forward = planarVec.normalized;
            else
                model.transform.forward = transform.forward;

            if (!lockPlanar)
                planarVec = pi.Dvec * walkSpeed * ((pi.Run && am.TryDoRun()) ? runMulti : 1.0f);
        }
        else
        {
            anim.SetFloat("right", 0);
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.Run && am.TryDoRun()) ? 2.0f : 1.0f), 0.5f));

            if (pi.Dmag > 0.1f && pi.inputEnabled)
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);

            if (!lockPlanar)
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.Run && am.TryDoRun()) ? runMulti : 1.0f);
        }
    }

    void Jump()
    {
        if (pi.Jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }
    }

    void Roll()
    {
        if (pi.Roll && am.TryDoRoll())
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (rigid.velocity.magnitude > rollVelocityThreshold)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }
    }

    void Attack()
    {
        if (leftIsShield)
        {
            if (CheckAnimatorStateWithName("ground") || CheckAnimatorStateWithName("blocked"))
            {
                anim.SetBool("defense", pi.Defense);
                anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 1.0f);
            }
            else
            {
                anim.SetBool("defense", false);
                anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 0);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 0);
        }

        if (canAttack && (CheckAnimatorStateWithName("ground") || CheckAnimatorStateWithTag("attackL") || CheckAnimatorStateWithTag("attackR")))
        {
            if (pi.LeftAttack || pi.RightAttack)
            {
                if (pi.LeftAttack && !leftIsShield && am.TryDoAttack())
                {
                    anim.SetBool("leftHandAttack", true);
                    anim.SetTrigger("attack");
                }
                else if (pi.RightAttack && am.TryDoAttack())
                {
                    anim.SetBool("leftHandAttack", false);
                    anim.SetTrigger("attack");
                }
            }

            if (pi.LeftHeavyAttack || pi.RightHeavyAttack)
            {
                if (pi.RightHeavyAttack && am.TryDoHeavyAttack())
                {
                    //right heavy attack
                }
                else
                {
                    if (!leftIsShield)
                    {
                        //left heavy attack
                    }
                    else if (am.TryDoHeavyAttack())
                    {
                        anim.SetTrigger("counterBack");
                    }
                }
            }
        }
    }

    void Action()
    {
        if (pi.Action)
            OnActionPressed.Invoke();
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrushVec;
        thrushVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    //Sensor 消息
    public void OnGroundSensor(bool isGround)
    {
        anim.SetBool("isGround", isGround);
    }

    //base layer 动画层消息
    void OnJumpEnter()
    {
        trackDirection = true;
        thrushVec.y = jumpVelocity;
    }

    void OnRollEnter()
    {
        trackDirection = true;
        thrushVec.y = rollVelocity;
        am.StateM.CountVigor(-am.StateM.state.rollCost);
    }

    void OnJabEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        lockPlanar = true;
    }

    void OnJabUpdate()
    {
        thrushVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    void OnGroundEnter()
    {
        col.material = fricitionOne;
        canAttack = true;
        pi.inputEnabled = true;
        lockPlanar = false;
        trackDirection = false;
    }

    void OnGroundExit()
    {
        col.material = fricitionZero;
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    void OnHitEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }

    void OnBlockedEnter()
    {
        pi.inputEnabled = false;
    }

    void OnDieEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }

    void OnStunnedEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }

    void OnCounterBackEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }

    //发往WeaponManager
    void OnLockEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("CounterBackDisable");
        model.SendMessage("WeaponDisable");
    }

    void OnCounterBackExit()
    {
        model.SendMessage("CounterBackDisable");
    }

    void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }

    //attack layer 动画子状态消息
    void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
    }

    void OnAttack1hAUpdate()
    {
        thrushVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
    }

    void CountAttackVigorCost()
    {
        am.StateM.CountVigor(-am.StateM.state.attackCost);
    }

    void CountHeavyAttackVigorCost()
    {
        am.StateM.CountVigor(-am.StateM.state.attackCost);
    }

    //root motion值处理
    public void OnUpdateRM(Vector3 _deltaPos)
    {
        if (CheckAnimatorStateWithName("attack1hC"))
            deltaPos += 0.8f * deltaPos + 0.2f * _deltaPos;
    }
}
