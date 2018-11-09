using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LockAnimatorClip : PlayableAsset, ITimelineClipAsset
{
    public LockAnimatorBehaviour template = new LockAnimatorBehaviour();
    public ExposedReference<ActorManager> am;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LockAnimatorBehaviour>.Create(graph, template);
        LockAnimatorBehaviour clone = playable.GetBehaviour();
        clone.am = am.Resolve(graph.GetResolver());
        return playable;
    }
}
