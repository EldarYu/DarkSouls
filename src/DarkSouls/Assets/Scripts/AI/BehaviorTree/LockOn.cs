using System.Collections.Generic;
using System.Linq;
using System.Text;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class LockOn : Action
{
    public SharedGameObject target;
    private DummyIPlayerInput input;
    private IActorManager actorManager;
    public override void OnStart()
    {
        input = GetComponent<DummyIPlayerInput>();
        actorManager = GetComponent<ActorManager>();
        input.target = target.Value;
    }

    public override TaskStatus OnUpdate()
    {
        actorManager.ActorC.camcon.LockUnlock(target.Value);
        if (actorManager.IsLockState)
            return TaskStatus.Success;
        else
            return TaskStatus.Running;
    }

}

