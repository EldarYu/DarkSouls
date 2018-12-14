using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : IActorManager
{
    public ActorController ActorC { get; private set; }
    public StateManager StateM { get; private set; }

    public BattleManager BattleM { get; private set; }
    public WeaponManager WeaponM { get; private set; }
    public InteractionManager InteractionM { get; private set; }
    public DirectorManager DirectorM { get; private set; }
    public InventoryManager InventoryM { get; private set; }
    public IPlayerInput PlayerInput { get { return ActorC.pi; } }

    public bool LeftIsShield { get { return WeaponM.LeftIsShield; } }
    private void Awake()
    {
        BattleM = GetComponentInChildren<BattleManager>();
        ActorC = GetComponent<ActorController>();
        WeaponM = GetComponentInChildren<WeaponManager>();
        StateM = GetComponent<StateManager>();
        InteractionM = GetComponentInChildren<InteractionManager>();
        DirectorM = GetComponent<DirectorManager>();
        InventoryM = GetComponent<InventoryManager>();
        ActorC.OnActionPressed += DoAction;
    }

    public void SwitchWeapon(ItemData itemData, Direction direction)
    {
        if (itemData == null)
            return;
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
                    case EventType.OpenBox:
                        AddItem(ecastm.itemData, ecastm.itemCount);
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

    public void TryDoDamage(WeaponController targetWC, bool attackVaild, bool counterVaild)
    {
        if (attackVaild)
        {
            if (StateM.isCounterBackSuccess && counterVaild)
            {
                targetWC.wm.am.Stunned();
            }
            else if (StateM.isCounterBackFailure)
            {
                HitOrDie(false);
            }
            else if (StateM.isImmortal)
            {
                //无敌
            }
            else if (StateM.isDefense)
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

    private void HitOrDie(bool doHitAnimation = true)
    {
        if (StateM.state.HP <= 0)
        {

        }
        else
        {
            StateM.CountHp(-5);
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

    private void Stunned()
    {
        ActorC.IssueTrigger("stunned");
    }

    private void Blocked()
    {
        ActorC.IssueTrigger("blocked");
    }

    private void Hit()
    {
        ActorC.IssueTrigger("hit");
    }

    private void Die()
    {
        ActorC.IssueTrigger("die");
        ActorC.pi.inputEnabled = false;
        if (ActorC.camcon.lockState)
        {
            ActorC.camcon.LockUnlock();
            ActorC.camcon.lockDot.enabled = false;
        }
        ActorC.camcon.enabled = false;
    }
}
