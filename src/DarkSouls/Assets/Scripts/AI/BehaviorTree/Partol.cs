using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Partol : Action
{
    public SharedTransformList partolPoints;
    private DummyIPlayerInput input;
    public override void OnStart()
    {
        input = gameObject.GetComponent<DummyIPlayerInput>();
    }

    public override TaskStatus OnUpdate()
    {
        return base.OnUpdate();
    }
}

