using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : BoxManager
{
    private Lever lever;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        em = GetComponentInChildren<EventCasterManager>();
        lever = GetComponentInParent<Lever>();
        Init(true);
    }
    public void Init(bool active)
    {
        em.active = active;
        anim.SetBool("active", active);
    }

    public override void UpOrDown()
    {
        lever.UpOrDown();
    }
}
