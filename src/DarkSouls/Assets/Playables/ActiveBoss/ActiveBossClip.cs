using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ActiveBossClip : PlayableAsset, ITimelineClipAsset
{
    public ActiveBossBehaviour template = new ActiveBossBehaviour();
    public ExposedReference<IActorManager> am;
    public ExposedReference<Camera> cam;
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ActiveBossBehaviour>.Create(graph, template);
        ActiveBossBehaviour clone = playable.GetBehaviour();
        clone.cam = cam.Resolve(graph.GetResolver());
        clone.am = am.Resolve(graph.GetResolver());
        return playable;
    }
}
