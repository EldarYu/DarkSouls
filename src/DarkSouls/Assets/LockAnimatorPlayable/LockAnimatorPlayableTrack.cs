using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1f, 0f, 0f)]
[TrackClipType(typeof(LockAnimatorPlayableClip))]
[TrackBindingType(typeof(ActorManager))]
public class LockAnimatorPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<LockAnimatorPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}
