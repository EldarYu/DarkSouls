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
        am.ECMOn();
        canAttack = false;
        am.lockDir = false;
        am.lockAgent = true;
    }

    void OnStunnedExit()
    {
        am.ECMOff();
        canAttack = true;
        am.lockAgent = false;
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
    }

    void Attack1Enter()
    {
        canAttack = false;
    }

    void Attack1Update()
    {
        thrushVec = model.transform.forward * anim.GetFloat("attack1VelocityForward");
    }

    void Attack1Exit()
    {
        canAttack = true;
    }

    void Attack2Enter()
    {
        canAttack = false;
    }

    void Attack2Update()
    {
        thrushVec = model.transform.forward * anim.GetFloat("attack2VelocityForward");
    }

    void Attack2Exit()
    {
        canAttack = true;
    }

    void ChargeEnter()
    {
        am.WeaponM.WeaponDisable();
        canAttack = false;
        am.lockAgent = true;
        am.lockDir = true;
    }

    void ChargeExit()
    {
        canAttack = true;
        am.IsChargeEnd = true;
        am.lockAgent = false;
        am.lockDir = false;
    }

    void LocolMotionEnter()
    {
        am.WeaponM.WeaponDisable();
        am.lockAgent = false;
        am.lockDir = false;
    }

    void LocolMotionExit()
    {
        am.lockAgent = true;
        am.lockDir = true;
    }
}
