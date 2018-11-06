using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public float maxHp;
    public float hp;

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

    [Header("2nd order state flag")]
    public bool isAllowDefense;
    public bool isImmortal;

    private ActorManager am;
    void Awake()
    {
        am = GetComponent<ActorManager>();
        hp = maxHp;
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

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckAnimatorStateWithName("defense1h", "Defense");
        isImmortal = isRoll || isJab;
    }


    public void CountHp(float amount)
    {
        hp += amount;
        hp = Mathf.Clamp(hp, 0, maxHp);
    }
}
