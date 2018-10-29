using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{
    private Animator anim;
    private ActorController ac;

    void Awake()
    {
        anim = GetComponent<Animator>();
        ac = transform.parent.GetComponent<ActorController>();
    }

    private void OnAnimatorMove()
    {
        ac.OnUpdateRM(anim.deltaPosition);
    }
}
