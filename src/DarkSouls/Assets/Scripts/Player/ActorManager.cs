using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    [HideInInspector]
    public ActorController ac;
    [HideInInspector]
    public StateManager sm;
    private BattleManager bm;
    private WeaponManager wm;

    private void Awake()
    {
        bm = GetComponentInChildren<BattleManager>();
        ac = GetComponent<ActorController>();
        wm = GetComponentInChildren<WeaponManager>();
        sm = GetComponent<StateManager>();
    }

    public void TryDoDamage(WeaponController targetWC)
    {
        if (sm.isCounterBackSuccess)
        {
            targetWC.wm.am.Stunned();
        }
        else if (sm.isCounterBackFailure)
        {

        }
        else if (sm.isImmortal)
        {

        }
        else if (sm.isDefense)
        {
            Blocked();
        }
        else if (sm.hp <= 0)
        {

        }
        else
        {
            sm.CountHp(-5);
            if (sm.hp > 0)
            {
                Hit();
            }
            else
            {
                Die();
            }
        }
    }

    public void SetCounterBackEnable(bool enable)
    {
        sm.isCounterBackEnable = enable;
    }



    private void Stunned()
    {
        ac.IssueTrigger("stunned");
    }

    private void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    private void Hit()
    {
        ac.IssueTrigger("hit");
    }

    private void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnabled = false;
        if (ac.camcon.lockState)
        {
            ac.camcon.LockOnLock();
            ac.camcon.lockDot.enabled = false;
        }
        ac.camcon.enabled = false;
    }
}
