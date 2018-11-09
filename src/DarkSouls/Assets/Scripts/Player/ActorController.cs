using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    [Header("Move Options")]
    public float walkSpeed = 1.7f;
    public float runMulti = 2.0f;
    public float jumpVelocity = 4.0f;
    public float rollVelocity = 3.0f;
    public float rollVelocityThreshold = 7.0f;

    [Header("Physic")]
    public PhysicMaterial fricitionOne;
    public PhysicMaterial fricitionZero;

    [HideInInspector]
    public IPlayerInput pi;
    [HideInInspector]
    public GameObject model;
    [HideInInspector]
    public CameraController camcon;
    public bool leftIsShield = true;

    public delegate void OnActionDelegate();
    public event OnActionDelegate OnActionPressed;

    private Animator anim;
    private Rigidbody rigid;
    private CapsuleCollider col;
    private Vector3 planarVec;
    private Vector3 deltaPos;
    private Vector3 thrushVec;
    private bool lockPlanar = false;
    private bool trackDirection = false;
    private bool canAttack;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        pi = GetInputDevice();
        col = GetComponent<CapsuleCollider>();
        camcon = GetComponentInChildren<CameraController>();

        model = transform.GetChild(0).gameObject;
        anim = model.GetComponent<Animator>();

        if (anim == null || rigid == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        if (camcon.lockState)
        {
            Vector3 localDevc = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forward", localDevc.z * (pi.Run ? 2.0f : 1.0f));
            anim.SetFloat("right", localDevc.x * (pi.Run ? 2.0f : 1.0f));

            if (trackDirection)
                model.transform.forward = planarVec.normalized;
            else
                model.transform.forward = transform.forward;

            if (!lockPlanar)
                planarVec = pi.Dvec * walkSpeed * (pi.Run ? runMulti : 1.0f);
        }
        else
        {
            anim.SetFloat("right", 0);
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.Run ? 2.0f : 1.0f), 0.5f));

            if (pi.Dmag > 0.1f)
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);

            if (!lockPlanar)
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.Run ? runMulti : 1.0f);
        }

        if (pi.Jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if (pi.Roll || rigid.velocity.magnitude > rollVelocityThreshold)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

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
                if (pi.LeftAttack && !leftIsShield)
                {
                    anim.SetBool("leftHandAttack", true);
                    anim.SetTrigger("attack");
                }
                else if (pi.RightAttack)
                {
                    anim.SetBool("leftHandAttack", false);
                    anim.SetTrigger("attack");
                }
            }

            if (pi.LeftHeavyAttack || pi.RightHeavyAttack)
            {
                if (pi.RightHeavyAttack)
                {
                    //right heavy attack
                }
                else
                {
                    if (!leftIsShield)
                    {
                        //left heavy attack
                    }
                    else
                    {
                        anim.SetTrigger("counterBack");
                    }
                }
            }
        }

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

    public bool CheckAnimatorStateWithName(string stateName, string layerName = "Base")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    public bool CheckAnimatorStateWithTag(string tagName, string layerName = "Base")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void SetAnimatorBool(string fieldName, bool value)
    {
        anim.SetBool(fieldName, value);
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

    public Animator GetAnimator()
    {
        return anim;
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

    //root motion值处理
    public void OnUpdateRM(Vector3 _deltaPos)
    {
        if (CheckAnimatorStateWithName("attack1hC"))
            deltaPos += 0.8f * deltaPos + 0.2f * _deltaPos;
    }

}
