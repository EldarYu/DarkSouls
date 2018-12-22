using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArtoriasManager : IActorManager
{
    public GameObject target;
    public float distance;
    public bool isChargeEnd = false;
    public NavMeshAgent agent;

    private float forward;
    void Awake()
    {
        ActorC = GetComponent<IActorController>();
        agent = GetComponent<NavMeshAgent>();
        WeaponM = GetComponentInChildren<WeaponManager>();
        //
        TestFun();
    }

    void TestFun()
    {
        target = GameObject.FindWithTag("Player");
        SetTarget();
    }

    void Update()
    {
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            transform.LookAt(target.transform);
        }

        if (distance != 0)
        {
            if (distance > 7.0f)
            {
                SetTarget();
                agent.speed = 3.0f;
            }

            else if (distance > 3.0f)
            {
                SetTarget();
                agent.speed = 1.0f;
            }
        }

        forward = agent.velocity.magnitude;
        ActorC.anim.SetFloat("forward", forward);
    }

    public void SetTarget()
    {
        if (target == null)
            return;
        agent.SetDestination(target.transform.position);
    }

    public void StabAttack()
    {
        ActorC.IssueTrigger("stab_attack");
    }

    public void Attack()
    {
        ActorC.IssueTrigger("attack");
    }

    public void JumpAttack()
    {
        ActorC.IssueTrigger("jump_attack");
    }

    public void Charge()
    {
        ActorC.SetAnimatorBool("charge", true);
    }


}
