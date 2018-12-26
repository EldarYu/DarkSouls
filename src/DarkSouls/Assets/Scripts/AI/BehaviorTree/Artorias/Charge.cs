using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Charge : Action
{
    private ArtoriasManager ArtorM;
    public override void OnStart()
    {
        ArtorM = GetComponent<ArtoriasManager>();
        if (!ArtorM.IsChargeEnd)
            ArtorM.Charge();
    }

    public override TaskStatus OnUpdate()
    {
        if (ArtorM.IsCharging)
            return TaskStatus.Running;

        if (ArtorM.IsChargeBreaked || ArtorM.IsChargeEnd)
            return TaskStatus.Failure;

        return TaskStatus.Running;
    }
}
