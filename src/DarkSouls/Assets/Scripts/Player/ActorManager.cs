using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : IActorManager
{
    [HideInInspector]
    public ActorController ac;
    [HideInInspector]
    public StateManager sm;
    private BattleManager bm;
    private WeaponManager wm;
    private InteractionManager im;
    private DirectorManager dm;
    private void Awake()
    {
        bm = GetComponentInChildren<BattleManager>();
        ac = GetComponent<ActorController>();
        wm = GetComponentInChildren<WeaponManager>();
        sm = GetComponent<StateManager>();
        im = GetComponentInChildren<InteractionManager>();
        dm = GetComponent<DirectorManager>();
        ac.OnActionPressed += DoAction;
    }

    public void DoAction()
    {
        foreach (var ecastm in im.overlapEcastms)
        {
            if (!ecastm.active)
                continue;

            switch (ecastm.eventType)
            {
                case EventType.OpenBox:
                    if (ac.model.transform.CheckAngleSelf(ecastm.am.transform, 30.0f))
                    {
                        transform.position = ecastm.transform.position + ecastm.transform.TransformVector(ecastm.offset);
                        ac.model.transform.LookAt(ecastm.transform);
                        ecastm.active = false;
                        dm.PlayeOpenBox(this, ecastm.am);
                    }
                    break;
                case EventType.FrontStab:
                    dm.PlayFrontStab(this, ecastm.am);
                    break;
                default:
                    break;
            }
        }
    }

    public void TryDoDamage(WeaponController targetWC, bool attackVaild, bool counterVaild)
    {
        if (attackVaild)
        {
            if (sm.isCounterBackSuccess && counterVaild)
            {
                targetWC.wm.am.Stunned();
            }
            else if (sm.isCounterBackFailure)
            {
                HitOrDie(false);
            }
            else if (sm.isImmortal)
            {
                //无敌
            }
            else if (sm.isDefense)
            {
                Blocked();
            }
            else
            {
                HitOrDie();
            }
        }
    }

    public void SetCounterBackEnable(bool enable)
    {
        sm.isCounterBackEnable = enable;
    }

    public override void LockUnlockAnimator(bool value = true)
    {
        ac.SetAnimatorBool("lock", value);
    }

    public override Animator GetAnimator()
    {
        return ac.GetAnimator();
    }

    private void HitOrDie(bool doHitAnimation = true)
    {
        if (sm.hp <= 0)
        {

        }
        else
        {
            sm.CountHp(-5);
            if (sm.hp > 0)
            {
                if (doHitAnimation)
                    Hit();
            }
            else
            {
                Die();
            }
        }
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
            ac.camcon.LockUnlock();
            ac.camcon.lockDot.enabled = false;
        }
        ac.camcon.enabled = false;
    }
}
