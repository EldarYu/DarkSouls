using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    [HideInInspector]
    public ActorController ac;
    private BattleManager bm;
    private WeaponManager wm;
    private StateManager sm;
    private void Awake()
    {
        bm = GetComponentInChildren<BattleManager>();
        ac = GetComponent<ActorController>();
        wm = GetComponentInChildren<WeaponManager>();
        sm = GetComponent<StateManager>();
    }

    public void TryDoDamage()
    {
        if (sm.hp == 0)
            return;

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

    private void Hit()
    {
        ac.IssueTrigger("hit");
    }

    private void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnabled = false;
        if (ac.camcon.lockState)
            ac.camcon.LockOnLock();
        ac.camcon.enabled = false;
    }
}
