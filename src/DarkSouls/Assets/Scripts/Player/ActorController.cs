using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class ActorController : MonoBehaviour
{
    [Header("Animator Field")]
    public string velocityFloat;
    public string jumpTrigger;
    public string isGroundBool;
    public string rollTrigger;
    public string attackTrigger;
    public string defenseBool;

    [Header("Animator Curves")]
    public string jabVelocity;
    public string attack1hAVelocity;

    [Header("Animator Layer Name")]
    public string attackLayer;

    [Header("Animator State Name")]
    public string threeStageAttack1h;

    [Header("Animator State Option")]
    public string mirror;

    [Header("Move Options")]
    public float walkSpeed;
    public float runMulti;
    public float jumpVelocity;
    public float rollVelocity;
    public float rollVelocityThreshold;

    [Header("Physic")]
    public PhysicMaterial fricitionOne;
    public PhysicMaterial fricitionZero;

    [HideInInspector]
    public IPlayerInput pi;
    [HideInInspector]
    public GameObject model;

    private Animator anim;
    private Rigidbody rigid;
    private CapsuleCollider col;

    private float layerLerpTarget;
    private Vector3 planarVec;
    private Vector3 deltaPos;
    private Vector3 thrushVec;
    private bool lockPlanar = false;
    private bool canAttack;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        pi = GetInputDevice();
        col = GetComponent<CapsuleCollider>();

        model = transform.GetChild(0).gameObject;
        anim = model.GetComponent<Animator>();

        if (anim == null || rigid == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        anim.SetFloat(velocityFloat, pi.Dmag * Mathf.Lerp(anim.GetFloat(velocityFloat), (pi.Run ? 2.0f : 1.0f), 0.5f));

        anim.SetBool(defenseBool, pi.Defense);

        if (pi.Jump)
        {
            anim.SetTrigger(jumpTrigger);
            canAttack = false;
        }

        if (pi.Roll || rigid.velocity.magnitude > rollVelocityThreshold)
        {
            anim.SetTrigger(rollTrigger);
            canAttack = false;
        }

        if (pi.Attack && canAttack)
            anim.SetTrigger(attackTrigger);

        if (pi.Dmag > 0.1f)
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);

        if (!lockPlanar)
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.Run ? runMulti : 1.0f);
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrushVec;
        thrushVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    private bool CheckAnimatorState(string stateName, int layerIndex = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
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
        anim.SetBool(isGroundBool, isGround);
    }

    //root motion值处理
    public void OnUpdateRM(Vector3 _deltaPos)
    {
        if (CheckAnimatorState(threeStageAttack1h, anim.GetLayerIndex(attackLayer)))
            deltaPos += 0.8f * deltaPos + 0.2f * _deltaPos;
    }

    //base layer 动画层消息
    void OnJumpEnter()
    {
        thrushVec.y = jumpVelocity;
    }

    void OnRollEnter()
    {
        thrushVec.y = rollVelocity;
    }

    void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    void OnJabUpdate()
    {
        thrushVec = model.transform.forward * anim.GetFloat(jabVelocity);
    }

    void OnGroundEnter()
    {
        col.material = fricitionOne;
        canAttack = true;
        pi.inputEnabled = true;
        lockPlanar = false;
    }

    void OnGroundExit()
    {
        col.material = fricitionZero;
        pi.inputEnabled = false;
        lockPlanar = true;
    }


    //attack layer 动画层消息
    void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        layerLerpTarget = 0;
    }

    void OnAttackIdleUpdate()
    {
        anim.SetLayerWeight(anim.GetLayerIndex(attackLayer),
         Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex(attackLayer)), layerLerpTarget, 0.4f));
    }

    void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
        layerLerpTarget = 1.0f;
    }

    void OnAttack1hAUpdate()
    {
        thrushVec = model.transform.forward * anim.GetFloat(attack1hAVelocity);
        anim.SetLayerWeight(anim.GetLayerIndex(attackLayer),
            Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex(attackLayer)), layerLerpTarget, 0.4f));
    }


}
