using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : IActorManager
{
    private Animator anim;
    private EventCasterManager em;
    private Lever lever;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        em = GetComponentInChildren<EventCasterManager>();
        lever = GetComponentInParent<Lever>();
        Init(true);
    }

    public override void LockUnlockAnimator(bool value = true)
    {
        anim.SetBool("lock", value);
    }

    public override Animator GetAnimator()
    {
        return anim;
    }

    public void Init(bool active)
    {
        em.active = active;
        anim.SetBool("active", active);
    }

    public override void UpOrDown()
    {
        lever.UpOrDown(false);
    }
}
