using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtoriasController : IActorController
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

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        camcon = GetComponentInChildren<CameraController>();
        model = transform.GetChild(0).gameObject;
        anim = model.GetComponent<Animator>();
        pi = GetComponent<IPlayerInput>();

        if (anim == null || rigid == null || camcon == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        LocolMotion();
        Roll();
        //attack
    }

    private void LocolMotion()
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

            if (pi.Dmag > 0.1f && pi.inputEnabled)
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);

            if (!lockPlanar)
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.Run ? runMulti : 1.0f);
        }
    }

    void Roll()
    {
        if (pi.Roll)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrushVec;
        thrushVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    //
    void OnGroundEnter()
    {
        pi.enabled = true;
    }

    void OnGroundExit()
    {
        pi.enabled = false;
    }

    void OnRollUpdate()
    {
        thrushVec.y = 0.1f;
    }
}
