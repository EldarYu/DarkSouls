using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : IActorManager
{
    private Animator anim;
    private EventCasterManager em;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        em = GetComponentInChildren<EventCasterManager>();

    }

    private void Update()
    {
        anim.SetBool("active", em.active);
    }

    public override void LockUnlockAnimator(bool value = true)
    {
        anim.SetBool("lock", value);
    }

    public override Animator GetAnimator()
    {
        return anim;
    }
}
