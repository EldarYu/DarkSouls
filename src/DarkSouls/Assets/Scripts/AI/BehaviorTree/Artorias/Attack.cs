using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Attack : Action
{
    public string AttackAnimtionName;
    private ArtoriasManager ArtorM;
    public override void OnStart()
    {
        ArtorM = GetComponent<ArtoriasManager>();
    }

    public override TaskStatus OnUpdate()
    {
        ArtorM.ActorC.IssueTrigger(AttackAnimtionName);
        return TaskStatus.Success;
    }
}

