using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private bool canAttack;
    private ArtoriasManager am;
    void Awake()
    {
        am = GetComponent<ArtoriasManager>();
        rigid = GetComponent<Rigidbody>();
        model = transform.GetChild(0).gameObject;
        anim = model.GetComponent<Animator>();
        if (anim == null || rigid == null)
            this.enabled = false;
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = thrushVec;
        thrushVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    void OnStunnedEnter()
    {
        am.ECMOn();
    }

    void OnStunnedExit()
    {
        am.ECMOff();
    }
}
