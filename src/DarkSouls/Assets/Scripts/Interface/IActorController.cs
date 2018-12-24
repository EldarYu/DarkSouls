using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class IActorController : MonoBehaviour
{
    [HideInInspector]
    public IPlayerInput pi;
    [HideInInspector]
    public GameObject model;
    [HideInInspector]
    public CameraController camcon;
    protected Animator anim;
    public delegate void OnActionHandle();
    public event OnActionHandle OnActionPressed;
    public void ActionPressed()
    {
        if (OnActionPressed != null)
            OnActionPressed.Invoke();
    }
    public virtual bool CheckAnimatorStateWithName(string stateName, string layerName = "Base")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    public virtual bool CheckAnimatorStateWithTag(string tagName, string layerName = "Base")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
    }

    public virtual void SetAnimatorFloat(string fieldName, float value)
    {
        anim.SetFloat(fieldName, value);
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
