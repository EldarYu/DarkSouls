using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DirectorManager : MonoBehaviour
{
    public TimelineAsset frontStab;
    public TimelineAsset openBox;
    public TimelineAsset leverUp;

    private ActorManager am;
    private PlayableDirector pd;
    private bool lastState = false;
    void Start()
    {
        am = GetComponent<ActorManager>();
        pd = GetComponent<PlayableDirector>();
    }

    public bool IsPlaying()
    {
        return pd.state == PlayState.Playing ? true : false;
    }

    public void Play(EventType eventType, IActorManager player, IActorManager opponent)
    {
        if (IsPlaying())
            return;

        pd.playableAsset = GetTimelineAsset(eventType);
        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Player Animation")
            {
                pd.SetGenericBinding(track, player.GetAnimator());
            }
            else if (track.name == "Opponent Animation")
            {
                pd.SetGenericBinding(track, opponent.GetAnimator());
            }
            else if (track.name == "Player Script")
            {
                pd.SetGenericBinding(track, player);
                foreach (var clip in track.GetClips())
                {
                    LockAnimatorClip lockClip = (LockAnimatorClip)clip.asset;
                    lockClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(lockClip.am.exposedName, player);
                }
            }
            else if (track.name == "Opponent Script")
            {
                pd.SetGenericBinding(track, opponent);
                foreach (var clip in track.GetClips())
                {
                    LockAnimatorClip lockClip = (LockAnimatorClip)clip.asset;
                    lockClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(lockClip.am.exposedName, opponent);
                }
            }
        }
        pd.Evaluate();
        pd.Play();
    }

    TimelineAsset GetTimelineAsset(EventType eventType)
    {
        switch (eventType)
        {
            case EventType.OpenBox:
                return Instantiate(openBox);

            case EventType.FrontStab:
                return Instantiate(frontStab);

            case EventType.LeverUp:
                return Instantiate(leverUp);
        }
        return null;
    }
}
