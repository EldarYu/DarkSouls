using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [System.Serializable]
    public class State
    {
        public float maxHp;
        public float maxVigor;

        public float hp;
        public float vigor;

        public void Init()
        {
            hp = maxHp;
            vigor = maxVigor;
        }

        public void ReCalculate()
        {

        }
    }
    public State state;

    public float runCost = 1.0f;
    public float rollCost = 15.0f;
    public float attackCost = 5.0f;
    public float heavyAttackCost = 15.0f;

    public float vigorAutoRecoverTime = 1.0f;
    public float vigorAutoRecoverAmount = 0.3f;
    private Timer recoverVigorTimer = new Timer();

    public float Hp
    {
        get
        {
            return state.hp;
        }
        private set
        {
            state.hp = Mathf.Clamp(value, 0, state.maxHp);
        }
    }
    public float Vigor
    {
        get
        {
            return state.vigor;
        }
        private set
        {
            state.vigor = Mathf.Clamp(value, 0, state.maxVigor);
        }
    }

    [Header("1st order state flag")]
    public bool isGround;
    public bool isJump;
    public bool isRoll;
    public bool isFall;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDefense;
    public bool isBlocked;
    public bool isDie;
    public bool isCounterBack;
    public bool isCounterBackEnable;

    [Header("2nd order state flag")]
    public bool isAllowDefense;
    public bool isImmortal;
    public bool isCounterBackSuccess;
    public bool isCounterBackFailure;

    private ActorManager am;
    void Awake()
    {
        am = GetComponent<ActorManager>();
        state.Init();
    }

    private void Update()
    {
        isGround = am.ac.CheckAnimatorStateWithName("ground");
        isJump = am.ac.CheckAnimatorStateWithName("jump");
        isRoll = am.ac.CheckAnimatorStateWithName("roll");
        isJab = am.ac.CheckAnimatorStateWithName("jab");
        isFall = am.ac.CheckAnimatorStateWithName("fall");
        isAttack = am.ac.CheckAnimatorStateWithTag("attackL") || am.ac.CheckAnimatorStateWithTag("attackR");
        isHit = am.ac.CheckAnimatorStateWithName("hit");
        isBlocked = am.ac.CheckAnimatorStateWithName("blocked");
        isDie = am.ac.CheckAnimatorStateWithName("die");
        isCounterBack = am.ac.CheckAnimatorStateWithName("counterBack");

        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckAnimatorStateWithName("defense1h", "Defense");
        isImmortal = isRoll || isJab;
    }

    private void FixedUpdate()
    {
        recoverVigorTimer.Tick(Time.fixedDeltaTime);
        if (recoverVigorTimer.IsFinished())
        {
            Vigor += vigorAutoRecoverAmount;
            if (Vigor >= state.maxVigor)
            {
                recoverVigorTimer.Stop();
            }
        }
    }

    public void CountHp(float amount)
    {
        Hp += amount;
    }

    public void CountVigor(float amount)
    {
        Vigor += amount;
        recoverVigorTimer.Go(vigorAutoRecoverTime);
    }
}
