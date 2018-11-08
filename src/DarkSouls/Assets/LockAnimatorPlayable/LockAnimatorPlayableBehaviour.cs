using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LockAnimatorPlayableBehaviour : PlayableBehaviour
{
    public string trackName;
    PlayableDirector pd;
    public override void OnPlayableCreate(Playable playable)
    {

    }
    public override void OnGraphStart(Playable playable)
    {
        pd = (PlayableDirector)playable.GetGraph().GetResolver();
        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == trackName)
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                Debug.Log(am);
            }
         
        }
    }

}
