using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class HasTarget : Conditional
{
    private ArtoriasManager ArtorM;
    public override void OnStart()
    {
        ArtorM = GetComponent<ArtoriasManager>();
    }

    public override TaskStatus OnUpdate()
    {
        if (ArtorM.target == null)
            return TaskStatus.Running;
        else
            return TaskStatus.Success;
    }
}

