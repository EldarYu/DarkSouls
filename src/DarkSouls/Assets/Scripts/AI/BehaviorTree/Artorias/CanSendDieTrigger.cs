using System.Collections.Generic;
using System.Linq;
using System.Text;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CanSendDieTrigger : Conditional
{
    private ArtoriasManager ArtorM;
    public override void OnStart()
    {
        ArtorM = GetComponent<ArtoriasManager>();
    }

    public override TaskStatus OnUpdate()
    {
        if (ArtorM.CanSendDieTrigger)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}

