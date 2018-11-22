using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : IActorController
{
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
        pi = GetInputDevice();
        col = GetComponent<CapsuleCollider>();
        this.enabled = !(pi == null || Init());
    }

    private void Update()
    {
        LocolMotion();
        Jump();
        Roll();
        Attack();
        Action();

        if (anim.GetFloat("forward") > 1.9f)
            am.sm.CountVigor(-am.sm.runCost);
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

    public void ResetInputDevice(IPlayerInput playerInput)
    {
        pi = playerInput;
    }

    public IPlayerInput GetInputDevice(bool isActive = true)
    {
        IPlayerInput[] devices = GetComponents<IPlayerInput>();
        foreach (var device in devices)
        {
            if (isActive)
            {
                if (device.enabled)
                    return device;
            }
            else
            {
                if (!device.enabled)
                    return device;
            }
        }
        return null;
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
        am.sm.CountVigor(-am.sm.rollCost);
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
        am.sm.CountVigor(-am.sm.attackCost);
    }

    void CountHeavyAttackVigorCost()
    {
        am.sm.CountVigor(-am.sm.attackCost);
    }

    //root motion值处理
    public void OnUpdateRM(Vector3 _deltaPos)
    {
        if (CheckAnimatorStateWithName("attack1hC"))
            deltaPos += 0.8f * deltaPos + 0.2f * _deltaPos;
    }
}
