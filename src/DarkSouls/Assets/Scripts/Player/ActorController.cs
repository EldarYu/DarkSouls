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

    [Header("Animator Layer Name")]
    public string attackLayer;

    [Header("Move Options")]
    public float walkSpeed;
    public float runMulti;
    public float jumpVelocity;
    public float rollVelocity;

    private GameObject model;
    private Animator animator;
    private Rigidbody rigid;
    private IPlayerInput pi;

    private Vector3 planarVec;
    private bool lockPlanar = false;
    private Vector3 thrushVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        pi = GetComponent<IPlayerInput>();

        model = transform.GetChild(0).gameObject;
        animator = model.GetComponent<Animator>();

        if (animator == null || rigid == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        animator.SetFloat(velocityFloat, pi.Dmag * Mathf.Lerp(animator.GetFloat(velocityFloat), (pi.Run ? 2.0f : 1.0f), 0.5f));

        if (pi.Jump)
            animator.SetTrigger(jumpTrigger);

        if (rigid.velocity.magnitude > 1.0f)
            animator.SetTrigger(rollTrigger);

        if (pi.Attack)
            animator.SetTrigger(attackTrigger);

        if (pi.Dmag > 0.1f)
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);

        if (!lockPlanar)
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.Run ? runMulti : 1.0f);


    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrushVec;
        thrushVec = Vector3.zero;
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
        pi.inputEnabled = true;
        lockPlanar = false;
    }

    void OnGroundExit()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }


    //attack layer 动画层消息
    void OnAttackIdleEnter()
    {
        animator.SetLayerWeight(animator.GetLayerIndex(attackLayer), 0);
    }

    void OnAttack1hAEnter()
    {
        animator.SetLayerWeight(animator.GetLayerIndex(attackLayer), 1.0f);
    }
}
