using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArtoriasManager : IActorManager
{
    public float atk = 5.0f;
    public float chageFinalIncrement = 3.0f;
    public float CurHp
    {
        get
        {
            return bossHp;
        }
        private set
        {
            bossHp = Mathf.Clamp(value, 0, maxBossHp);
        }
    }
    public override bool IsDie()
    {
        return CurHp <= 0;
    }
    public bool trackTarget;
    public bool IsChargeEnd { get; private set; }
    public GameObject target;
    public float distance;
    public NavMeshAgent agent;
    public bool IsImmortal { get { return ActorC.CheckAnimatorStateWithName("charge"); } }
    public bool CanAttack { get { return ActorC.canAttack; } }
    private float forward;
    void Awake()
    {
        ActorC = GetComponent<IActorController>();
        agent = GetComponent<NavMeshAgent>();
        WeaponM = GetComponentInChildren<WeaponManager>();
        EventCastM = GetComponentInChildren<EventCasterManager>();
        CurHp = maxBossHp;
        IsChargeEnd = false;
    }

    public override void LockTarget(GameObject target)
    {
        this.target = target;
        trackTarget = true;
        SetTarget();
    }

    void Update()
    {
        if (IsDie())
            return;

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
        ActorC.SetAnimatorFloat("forward", forward);
    }

    public void SetTarget()
    {
        if (target == null && !trackTarget)
            return;
        agent.SetDestination(target.transform.position);
    }

    public void StabAttack()
    {
        if (ActorC.canAttack)
            ActorC.IssueTrigger("stab_attack");
    }

    public void Attack()
    {
        if (ActorC.canAttack)
            ActorC.IssueTrigger("attack");
    }

    public void JumpAttack()
    {
        if (ActorC.canAttack)
            ActorC.IssueTrigger("jump_attack");
    }

    public void Charge()
    {
        ActorC.SetAnimatorBool("charge", true);
    }

    public override void TryDoDamage(WeaponController targetWC, bool attackVaild, bool counterVaild)
    {
        if (attackVaild)
        {
            if (IsImmortal)
            {

            }
            else
            {
                HitOrDie(targetWC.Atk + targetWC.wm.am.GetAtk(), false);
            }
        }
    }

    public override float GetAtk()
    {
        return IsChargeEnd ? atk : atk * chageFinalIncrement;
    }

    public override void HitOrDie(float hitAmount, bool doHitAnimation = true)
    {
        if (CurHp <= 0)
        {

        }
        else
        {
            CurHp -= hitAmount;
            if (bossHp <= 0)
            {
                Die();
            }
        }
    }

    public override void Stunned()
    {
        ActorC.IssueTrigger("stunned");
    }

    public void Die()
    {
        ActorC.IssueTrigger("die");
    }

    public override void LockUnlockAnimator(bool value = true)
    {
        ActorC.SetAnimatorBool("lock", value);
    }

    public void ECMOn()
    {
        if (EventCastM != null)
            EventCastM.active = true;
    }

    public void ECMOff()
    {
        if (EventCastM != null)
            EventCastM.active = false;
    }

    public override Animator GetAnimator()
    {
        return ActorC.GetAnimator();
    }
}
