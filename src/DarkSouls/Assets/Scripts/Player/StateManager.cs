using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateManager : MonoBehaviour
{
    public State state;
    private Timer recoverVigorTimer = new Timer();

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
        state = Instantiate(state);
        state.Init();
        state.souls = 60000;
    }

    private void Update()
    {
        isGround = am.ActorC.CheckAnimatorStateWithName("ground");
        isJump = am.ActorC.CheckAnimatorStateWithName("jump");
        isRoll = am.ActorC.CheckAnimatorStateWithName("roll");
        isJab = am.ActorC.CheckAnimatorStateWithName("jab");
        isFall = am.ActorC.CheckAnimatorStateWithName("fall");
        isAttack = am.ActorC.CheckAnimatorStateWithTag("attackL") || am.ActorC.CheckAnimatorStateWithTag("attackR");
        isHit = am.ActorC.CheckAnimatorStateWithName("hit");
        isBlocked = am.ActorC.CheckAnimatorStateWithName("blocked");
        isDie = am.ActorC.CheckAnimatorStateWithName("die");
        isCounterBack = am.ActorC.CheckAnimatorStateWithName("counterBack");

        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ActorC.CheckAnimatorStateWithName("defense1h", "Defense");
        isImmortal = isRoll || isJab;
    }

    private void FixedUpdate()
    {
        recoverVigorTimer.Tick(Time.fixedDeltaTime);
        if (recoverVigorTimer.IsFinished())
        {
            state.Vigor += state.vigorAutoRecoverAmount;
            if (state.Vigor >= state.MaxVigor)
            {
                recoverVigorTimer.Stop();
            }
        }
    }

    public void CountHp(float amount)
    {
        state.HP += amount;
    }

    public void CountVigor(float amount, bool autoRecover = true)
    {
        state.Vigor += amount;
        if (autoRecover)
            recoverVigorTimer.Go(state.vigorAutoRecoverTime);
    }
}
