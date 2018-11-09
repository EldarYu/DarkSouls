using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LockAnimatorBehaviour : PlayableBehaviour
{
    public ActorManager am;
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        am.LockUnlockAnimator(false);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        am.LockUnlockAnimator();
    }
}
