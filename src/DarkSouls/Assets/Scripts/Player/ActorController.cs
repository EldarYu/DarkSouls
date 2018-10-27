using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    [Header("Animator Field")]
    public string velocityFloat;
    public string jumpTrigger;
    public string isGroundBool;
    public string rollTrigger;
    public string attackTrigger;

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

    [Header("Physic")]
    public PhysicMaterial fricitionOne;
    public PhysicMaterial fricitionZero;

    public IPlayerInput pi;
    public GameObject model;

    private Animator animator;
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
        animator = model.GetComponent<Animator>();

        if (animator == null || rigid == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        animator.SetFloat(velocityFloat, pi.Dmag * Mathf.Lerp(animator.GetFloat(velocityFloat), (pi.Run ? 2.0f : 1.0f), 0.5f));

        if (pi.Jump)
        {
            canAttack = false;
            animator.SetTrigger(jumpTrigger);
        }

        if (rigid.velocity.magnitude > 1.0f)
            animator.SetTrigger(rollTrigger);

        if (pi.Attack && canAttack)
            animator.SetTrigger(attackTrigger);

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
        return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
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


    //base layer 动画层消息
    void IsGround()
    {
        animator.SetBool(isGroundBool, true);
    }

    void IsNotGround()
    {
        animator.SetBool(isGroundBool, false);
    }

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
        thrushVec = model.transform.forward * animator.GetFloat(jabVelocity);
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
        animator.SetLayerWeight(animator.GetLayerIndex(attackLayer),
         Mathf.Lerp(animator.GetLayerWeight(animator.GetLayerIndex(attackLayer)), layerLerpTarget, 0.4f));
    }

    void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
        layerLerpTarget = 1.0f;
    }

    void OnAttack1hAUpdate()
    {
        thrushVec = model.transform.forward * animator.GetFloat(attack1hAVelocity);
        animator.SetLayerWeight(animator.GetLayerIndex(attackLayer),
            Mathf.Lerp(animator.GetLayerWeight(animator.GetLayerIndex(attackLayer)), layerLerpTarget, 0.4f));
    }


    //root motion值处理
    void OnUpdateRM(object _deltaPos)
    {
        if (CheckAnimatorState(threeStageAttack1h, animator.GetLayerIndex(attackLayer)))
            deltaPos += 0.8f * deltaPos + 0.2f * (Vector3)_deltaPos;
    }
}
