using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LockAnimatorPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public LockAnimatorPlayableBehaviour template = new LockAnimatorPlayableBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LockAnimatorPlayableBehaviour>.Create (graph, template);
        LockAnimatorPlayableBehaviour clone = playable.GetBehaviour ();
        return playable;
    }
}
