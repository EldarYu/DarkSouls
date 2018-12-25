using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CanAttack : Conditional
{
    private ArtoriasManager ArtorM;
    public override void OnStart()
    {
        ArtorM = GetComponent<ArtoriasManager>();
    }

    public override TaskStatus OnUpdate()
    {
        if (ArtorM.CanAttack)
            return TaskStatus.Success;
        else
            return TaskStatus.Running;
    }
}

