using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1f, 0f, 0.06206894f)]
[TrackClipType(typeof(LockAnimatorClip))]
public class LockAnimatorTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<LockAnimatorMixerBehaviour>.Create (graph, inputCount);
    }
}
