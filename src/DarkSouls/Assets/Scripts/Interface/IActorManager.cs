using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IActorManager : MonoBehaviour
{
    public IActorController ActorC { get; protected set; }
    public StateManager StateM { get; protected set; }

    public BattleManager BattleM { get; protected set; }
    public WeaponManager WeaponM { get; protected set; }
    public InteractionManager InteractionM { get; protected set; }
    public DirectorManager DirectorM { get; protected set; }
    public InventoryManager InventoryM { get; protected set; }
    public IPlayerInput PlayerInput { get { return ActorC.pi; } }
    public EventCasterManager EventCastM { get; protected set; }
    public bool IsLockState { get { return ActorC.camcon.lockState; } }

    public virtual Animator GetAnimator() { return null; }
    public virtual void LockUnlockAnimator(bool value = true) { }
    public virtual void HitOrDie(float hitAmount, bool doHitAnimation = true) { }
    public virtual void TryDoDamage(WeaponController targetWC, bool attackVaild, bool counterVaild) { }
    public virtual void SetCounterBackEnable(bool enable) { }
    public virtual float GetAtk() { return 0; }
    public virtual void Stunned() { }
}
