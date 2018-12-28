using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : IActorManager
{
    public bool LeftIsShield { get { return WeaponM.LeftIsShield; } }
    public delegate void Dead();
    public event Dead OnDead;

    private void Awake()
    {
        BattleM = GetComponentInChildren<BattleManager>();
        ActorC = GetComponent<ActorController>();
        WeaponM = GetComponentInChildren<WeaponManager>();
        StateM = GetComponent<StateManager>();
        InteractionM = GetComponentInChildren<InteractionManager>();
        DirectorM = GetComponent<DirectorManager>();
        InventoryM = GetComponent<InventoryManager>();
        EventCastM = GetComponentInChildren<EventCasterManager>();
        ActorC.OnActionPressed += DoAction;
    }

    public void SwitchWeapon(ItemData itemData, Direction direction)
    {
        if (itemData == null)
            return;

        WeaponM.SwitchWeapon(itemData, direction);
    }

    public void DoAction()
    {
        EventCasterManager waitForRemoveEcastm = null;
        foreach (var ecastm in InteractionM.overlapEcastms)
        {
            if (!ecastm.active)
                continue;

            if (ActorC.model.transform.CheckAngleSelf(ecastm.am.transform, 30.0f))
            {
                switch (ecastm.eventType)
                {
                    case EventCasterType.OpenDoor:
                        transform.position += transform.forward * 3;
                        ecastm.am.StartBossBattle(this.gameObject);
                        break;
                    case EventCasterType.LeverUp:
                        ecastm.am.UpOrDown();
                        break;
                    case EventCasterType.OpenBox:
                        AddItem(ecastm.itemData, ecastm.itemCount);
                        break;
                    case EventCasterType.FrontStab:
                        ecastm.am.HitOrDie(ecastm.am.maxBossHp * 0.2f, false);
                        break;
                }
                //transform.position = ecastm.transform.position + ecastm.am.transform.TransformVector(ecastm.offset);
                ActorC.model.transform.forward -= ecastm.am.transform.forward;
                //ac.model.transform.LookAt(ecastm.am.transform, Vector3.up);
                ecastm.active = false;
                waitForRemoveEcastm = ecastm;
                DirectorM.Play(ecastm.eventType, this, ecastm.am);
                break;
            }
        }
        if (waitForRemoveEcastm != null)
            InteractionM.overlapEcastms.Remove(waitForRemoveEcastm);
    }

    public void AddItem(ItemData data, int count)
    {
        InventoryM.Additem(data, count);
    }

    public bool UseItem(int index, int amount)
    {
        if (InventoryM.UseItem(index, amount))
        {
            if (!InventoryM[index].forSoul)
                WeaponM.ShowItem(InventoryM[index]);
            return true;
        }
        return false;
    }

    public void HideItem()
    {
        WeaponM.HideItem();
    }

    public bool CanDoAction()
    {
        return InteractionM.overlapEcastms.Count > 0;
    }

    public bool TryDoRun()
    {
        return StateM.state.Vigor > 0;
    }

    public bool TryDoRoll()
    {
        return StateM.state.Vigor > StateM.state.rollCost;
    }

    public bool TryDoAttack()
    {
        return StateM.state.Vigor > StateM.state.attackCost;
    }

    public bool TryDoHeavyAttack()
    {
        return StateM.state.Vigor > StateM.state.heavyAttackCost;
    }

    public bool TryDoBlcok()
    {
        return StateM.state.Vigor > StateM.state.blockCost;
    }

    public override void TryDoDamage(WeaponController targetWC, bool attackVaild, bool counterVaild)
    {
        if (attackVaild)
        {
            if (StateM.isCounterBackSuccess && counterVaild)
            {
                targetWC.wm.am.Stunned();
            }
            else if (StateM.isCounterBackFailure)
            {
                HitOrDie(targetWC.Atk + targetWC.wm.am.GetAtk(), false);
            }
            else if (StateM.isImmortal)
            {
                //无敌
            }
            else if (StateM.isDefense && TryDoBlcok())
            {
                Blocked();
            }
            else
            {
                HitOrDie(targetWC.Atk + targetWC.wm.am.GetAtk());
            }
        }
    }

    public override float GetAtk()
    {
        return StateM.state.Attack;
    }

    public override void SetCounterBackEnable(bool enable)
    {
        StateM.isCounterBackEnable = enable;
    }

    public override void LockUnlockAnimator(bool value = true)
    {
        ActorC.SetAnimatorBool("lock", value);
    }

    public override Animator GetAnimator()
    {
        return ActorC.GetAnimator();
    }

    public override void HitOrDie(float hitAmount, bool doHitAnimation = true)
    {
        if (StateM.state.HP <= 0)
        {

        }
        else
        {
            StateM.CountHp(-hitAmount);
            if (StateM.state.HP > 0)
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

    public override void Stunned()
    {
        ActorC.IssueTrigger("stunned");
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

    private void Blocked()
    {
        ActorC.IssueTrigger("blocked");
    }

    private void Hit()
    {
        ActorC.IssueTrigger("hit");
    }

    public void Die()
    {
        ActorC.IssueTrigger("die");
        ActorC.pi.inputEnabled = false;
        if (ActorC.camcon.lockState)
        {
            ActorC.camcon.LockUnlock();
            ActorC.camcon.lockDot.enabled = false;
        }
        ActorC.camcon.enabled = false;
        if (OnDead != null)
            OnDead.Invoke();
    }
}
