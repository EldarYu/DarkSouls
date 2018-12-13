using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    private Animator anim;
    private ActorManager am;
    void Awake()
    {
        am = GetComponentInParent<ActorManager>();
        anim = GetComponent<Animator>();
    }

    public void ResetTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }

    public void HideItem()
    {
        am.HideItem();
    }
}
