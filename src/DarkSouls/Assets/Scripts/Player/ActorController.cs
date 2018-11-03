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
    public string forwardFloat;
    public string rightFloat;
    public string jumpTrigger;
    public string isGroundBool;
    public string rollTrigger;
    public string attackTrigger;
    public string defenseBool;
    public string leftHAttackBool;
    public string hiTrigger;

    [Header("Animator Curves")]
    public string jabVelocity;
    public string attack1hAVelocity;

    [Header("Animator Layer Name")]
    public string attackLayer;
    public string defenseLayer;

    [Header("Animator State or Tag Name")]
    public string threeStageAttack1h;
    public string groundState;
    public string attackTag;

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
    public bool leftIsShield = true;

    private Animator anim;
    private Rigidbody rigid;
    private CapsuleCollider col;
    private CameraController camcon;
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
            anim.SetFloat(forwardFloat, localDevc.z * (pi.Run ? 2.0f : 1.0f));
            anim.SetFloat(rightFloat, localDevc.x * (pi.Run ? 2.0f : 1.0f));

            if (trackDirection)
                model.transform.forward = planarVec.normalized;
            else
                model.transform.forward = transform.forward;

            if (!lockPlanar)
                planarVec = pi.Dvec * walkSpeed * (pi.Run ? runMulti : 1.0f);
        }
        else
        {
            anim.SetFloat(rightFloat, 0);
            anim.SetFloat(forwardFloat, pi.Dmag * Mathf.Lerp(anim.GetFloat(forwardFloat), (pi.Run ? 2.0f : 1.0f), 0.5f));

            if (pi.Dmag > 0.1f)
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);

            if (!lockPlanar)
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.Run ? runMulti : 1.0f);
        }

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

        if ((pi.LeftAttack || pi.RightAttack) && canAttack && (CheckAnimatorStateWithName(groundState) || CheckAnimatorStateWithTag(attackTag)))
        {
            if (pi.LeftAttack && !leftIsShield)
            {
                anim.SetBool(leftHAttackBool, true);
                anim.SetTrigger(attackTrigger);
            }
            else if (pi.RightAttack)
            {
                anim.SetBool(leftHAttackBool, false);
                anim.SetTrigger(attackTrigger);
            }
        }

        if (leftIsShield)
        {
            anim.SetLayerWeight(anim.GetLayerIndex(defenseLayer), 1.0f);
            if (CheckAnimatorStateWithName(groundState))
            {
                anim.SetBool(defenseBool, pi.Defense);
            }
            else
            {
                anim.SetBool(defenseBool, false);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex(defenseLayer), 0);
        }
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrushVec;
        thrushVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    private bool CheckAnimatorStateWithName(string stateName, int layerIndex = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
    }

    private bool CheckAnimatorStateWithTag(string tagName, int layerIndex = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
    }

    public void SetHitTirgger()
    {
        anim.SetTrigger(hiTrigger);
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
    }

    //attack layer 动画子状态消息
    void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
    }

    void OnAttack1hAUpdate()
    {
        thrushVec = model.transform.forward * anim.GetFloat(attack1hAVelocity);
    }

    //root motion值处理
    public void OnUpdateRM(Vector3 _deltaPos)
    {
        if (CheckAnimatorStateWithName(threeStageAttack1h))
            deltaPos += 0.8f * deltaPos + 0.2f * _deltaPos;
    }

}
