using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class StabAttack : Attack
{
    public override TaskStatus OnUpdate()
    {
        ArtorM.StabAttack();
        return TaskStatus.Success;
    }
}

