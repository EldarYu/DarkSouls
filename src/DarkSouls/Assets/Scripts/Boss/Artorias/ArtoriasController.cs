using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArtoriasController : IActorController
{
    private Rigidbody rigid;
    private Vector3 thrushVec;
    private ArtoriasManager am;
    private NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        am = GetComponent<ArtoriasManager>();
        rigid = GetComponent<Rigidbody>();
        model = transform.GetChild(0).gameObject;
        anim = model.GetComponent<Animator>();
        if (anim == null || rigid == null)
            this.enabled = false;
    }

    private void FixedUpdate()
    {
        rigid.velocity = thrushVec;
        thrushVec = Vector3.zero;
    }

    //
    void OnStunnedEnter()
    {
        am.WeaponM.WeaponDisable();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        am.ECMOn();
        canAttack = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    void OnStunnedExit()
    {
        agent.isStopped = false;
        am.ECMOff();
        canAttack = true;
        agent.isStopped = false;
    }

    void StabAttackEnter()
    {
        canAttack = false;
    }

    void StabAttackUpdate()
    {
        thrushVec = model.transform.forward * anim.GetFloat("stab_attackVelocityForward");
    }

    void StabAttackExit()
    {
        canAttack = true;
    }

    void JumpAttackEnter()
    {
        canAttack = false;
    }

    void JumpAttackUpdate()
    {
        thrushVec = model.transform.forward * anim.GetFloat("jump_attackVelocityForward") +
            model.transform.up * anim.GetFloat("jump_attackVelocityUp");
    }

    void JumpAttackExit()
    {
        canAttack = true;
        am.Attack();
    }

    void Attack1Enter()
    {
        canAttack = false;
    }

    void Attack1Update()
    {
        thrushVec = model.transform.forward * anim.GetFloat("attackVelocityForward");
    }

    void Attack1Exit()
    {
        canAttack = true;
    }

    void ChargeEnter()
    {
        am.WeaponM.WeaponDisable();
        canAttack = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    void ChargeExit()
    {
        canAttack = true;
        am.IsChargeEnd = true;
        agent.isStopped = false;
    }

    void LocolMotionEnter()
    {
        am.WeaponM.WeaponDisable();
        am.trackTarget = true;
        agent.isStopped = false;
    }

    void LocolMotionExit()
    {
        am.trackTarget = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

}
