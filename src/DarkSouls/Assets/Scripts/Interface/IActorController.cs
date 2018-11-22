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
