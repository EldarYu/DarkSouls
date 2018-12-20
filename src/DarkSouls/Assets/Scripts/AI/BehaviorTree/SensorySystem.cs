using System.Text;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class SensorySystem : Conditional
{
    public SharedGameObject target;
    public SharedGameObject Hearing;
    public SharedGameObject Vision;
    public NoiseLevel noiseLevel;
    private SensoryComponent hearing;
    private SensoryComponent vision;
    public override void OnAwake()
    {
        hearing = Hearing.Value.GetComponent<SensoryComponent>();
        vision = Vision.Value.GetComponent<SensoryComponent>();
    }

    public override TaskStatus OnUpdate()
    {
        if (hearing.target != null && CanHear(hearing.target))
        {
            target.SetValue(hearing.target);
            return TaskStatus.Success;
        }

        if (vision.target != null)
        {
            target.SetValue(vision.target);
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public bool CanHear(GameObject target)
    {
        IHearable hearable = target.GetComponent<IHearable>();
        if (hearable == null)
            return false;
        return hearable.GetNoiseLevel() >= noiseLevel;
    }
}

