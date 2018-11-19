using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class IActorController : MonoBehaviour
{
    [Header("Move Options")]
    public float walkSpeed = 1.7f;
    public float runMulti = 2.0f;
    public float jumpVelocity = 4.0f;
    public float rollVelocity = 3.0f;
    public float rollVelocityThreshold = 7.0f;

    [HideInInspector]
    public IPlayerInput pi;
    [HideInInspector]
    public GameObject model;
    [HideInInspector]
    public CameraController camcon;

    protected Animator anim;
    protected Rigidbody rigid;
    protected Vector3 planarVec;
    protected Vector3 deltaPos;
    protected Vector3 thrushVec;
    protected bool lockPlanar = false;
    protected bool trackDirection = false;
    protected bool canAttack;

    protected virtual bool Init()
    {
        rigid = GetComponent<Rigidbody>();
        camcon = GetComponentInChildren<CameraController>();
        model = transform.GetChild(0).gameObject;
        anim = model.GetComponent<Animator>();
        return anim == null || rigid == null || camcon == null;
    }

    protected virtual void LocolMotion()
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

    public virtual bool CheckAnimatorStateWithName(string stateName, string layerName = "Base")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    public virtual bool CheckAnimatorStateWithTag(string tagName, string layerName = "Base")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
    }

    public virtual void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public virtual void SetAnimatorBool(string fieldName, bool value)
    {
        anim.SetBool(fieldName, value);
    }

    public virtual Animator GetAnimator()
    {
        return anim;
    }
}
