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
    private IActorManager am;
    public override void OnStart()
    {
        input = GetComponent<DummyIPlayerInput>();
        am = GetComponent<ActorManager>();
        input.target = target.Value;
    }

    public override TaskStatus OnUpdate()
    {
        am.LockTarget(target.Value);
        if (am.IsLockState)
            return TaskStatus.Success;
        else
            return TaskStatus.Running;
    }

}

