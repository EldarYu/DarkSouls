using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArtoriasManager : IActorManager
{
    public float atk = 5.0f;
    public float chageFinalIncrement = 3.0f;
    public float chageBreakedAmount = 600.0f;
    public float curChargeBreakedAmount;
    public bool IsChargeBreaked { get; private set; }
    public bool IsChargeEnd { get; set; }
    public bool IsCharging { get; private set; }
    public float runDistance = 7.0f;
    public float walkDistance = 3.0f;
    public float runSpeed = 3.0f;
    public float walkSpeed = 1.0f;
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

    public bool lockAgent;
    public bool lockDir;
    private Vector3 dir;
    public GameObject target;
    public float distance;
    public NavMeshAgent agent;
    public bool CanAttack { get { return ActorC.canAttack; } }
    private float forward;
    private float velocitySpeed;
    private bool sendedDieTrigger = false;
    void Awake()
    {
        ActorC = GetComponent<IActorController>();
        agent = GetComponent<NavMeshAgent>();
        WeaponM = GetComponentInChildren<WeaponManager>();
        EventCastM = GetComponentInChildren<EventCasterManager>();
        CurHp = maxBossHp;
        IsChargeEnd = false;
        IsCharging = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        lockDir = true;
        lockAgent = false;
        isDead = false;
    }

    public override void LockTarget(GameObject target)
    {
        this.target = target;
        SetTarget();
    }

    void Update()
    {
        if (isDead)
        {
            if (!sendedDieTrigger)
                Die();
            return;
        }

        IsCharging = ActorC.CheckAnimatorStateWithName("charge");
        if (IsCharging)
        {
            if (curChargeBreakedAmount >= chageBreakedAmount)
            {
                IsChargeBreaked = true;
                ActorC.IssueTrigger("chargeBreaked");
                IsChargeEnd = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            ActorC.canAttack = false;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            return;
        }

        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            dir = (target.transform.position - transform.position).normalized;
            if (lockDir)
            {
                ActorC.model.transform.forward = Vector3.Slerp(ActorC.model.transform.forward, dir, 0.8f);
                transform.forward = ActorC.model.transform.forward;
            }
        }

        if (distance != 0 && !lockAgent)
        {

            if (distance > runDistance)
            {
                ActorC.canAttack = false;
                agent.isStopped = false;
                agent.speed = Mathf.SmoothDamp(agent.speed, runSpeed, ref velocitySpeed, 0.3f);
                SetTarget();
            }
            else if (distance > walkDistance)
            {
                ActorC.canAttack = false;
                agent.isStopped = false;
                agent.speed = Mathf.SmoothDamp(agent.speed, walkSpeed, ref velocitySpeed, 0.3f);
                SetTarget();
            }
            else
            {
                ActorC.canAttack = true;
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
            }
        }
        else
        {
            ActorC.canAttack = true;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        forward = agent.velocity.magnitude;
        ActorC.SetAnimatorFloat("forward", forward);
    }

    public void SetTarget()
    {
        if (target == null)
            return;
        agent.SetDestination(target.transform.position);
    }


    public void Charge()
    {
        ActorC.IssueTrigger("charge");
    }

    public override void TryDoDamage(WeaponController targetWC, bool attackVaild, bool counterVaild)
    {
        if (attackVaild)
        {
            HitOrDie(targetWC.Atk + targetWC.wm.am.GetAtk(), false);
        }
    }

    public override float GetAtk()
    {
        return IsChargeEnd && !IsChargeBreaked ? atk * chageFinalIncrement : atk;
    }

    public override void HitOrDie(float hitAmount, bool doHitAnimation = true)
    {
        if (CurHp <= 0)
        {

        }
        else
        {
            if (IsCharging)
                curChargeBreakedAmount += hitAmount;
            CurHp -= hitAmount;
            if (bossHp <= 0)
            {
                isDead = true;
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
        sendedDieTrigger = true;
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
