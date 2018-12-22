using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class JumpAttack : Attack
{
    public override TaskStatus OnUpdate()
    {
        ArtorM.JumpAttack();
        return TaskStatus.Success;
    }
}

