using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class Partol : Action
{
    public SharedTransformList partolPoints;
    private DummyIPlayerInput input;
    private Vector3 curPoint;
    private int curPointIndex;
    private NavMeshAgent navMeshAgent;
    public override void OnStart()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        input = gameObject.GetComponent<DummyIPlayerInput>();
        curPointIndex = 0;
        curPoint = partolPoints.Value[curPointIndex].position;
        input.nextPoint = curPoint;
    }

    public override TaskStatus OnUpdate()
    {
        input.Move(NextPoint());
        return TaskStatus.Running;
    }

    private Vector3 NextPoint()
    {
        if (input.distance > 0.5f)
            return partolPoints.Value[curPointIndex].position;

        if (curPointIndex + 1 >= partolPoints.Value.Count)
            curPointIndex = 0;
        else
            curPointIndex += 1;

        return partolPoints.Value[curPointIndex].position;
    }
}

