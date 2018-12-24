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
    public TimelineAsset openDoor;
    public Camera animCam;
    private ActorManager am;
    private PlayableDirector pd;

    void Start()
    {
        am = GetComponent<ActorManager>();
        pd = GetComponent<PlayableDirector>();
    }

    public bool IsPlaying()
    {
        return pd.state == PlayState.Playing ? true : false;
    }

    public void Play(EventCasterType eventType, IActorManager player, IActorManager opponent)
    {
        if (IsPlaying())
            return;

        pd.playableAsset = GetTimelineAsset(eventType);
        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

        if (eventType == EventCasterType.OpenDoor)
        {

            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Player Animation")
                {
                    pd.SetGenericBinding(track, player);
                }
                else if (track.name == "Camera Animation")
                {
                    pd.SetGenericBinding(track, animCam);
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
                else if (track.name == "Boss Script")
                {
                    pd.SetGenericBinding(track, opponent);
                    foreach (var clip in track.GetClips())
                    {
                        ActiveBossClip activeClip = (ActiveBossClip)clip.asset;
                        activeClip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(activeClip.am.exposedName, opponent);
                        activeClip.cam.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(activeClip.cam.exposedName, animCam);
                    }
                }
            }
        }
        else
        {
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
        }

        pd.Evaluate();
        pd.Play();
    }

    TimelineAsset GetTimelineAsset(EventCasterType eventType)
    {
        switch (eventType)
        {
            case EventCasterType.OpenBox:
                return Instantiate(openBox);

            case EventCasterType.FrontStab:
                return Instantiate(frontStab);

            case EventCasterType.LeverUp:
                return Instantiate(leverUp);

            case EventCasterType.OpenDoor:
                return Instantiate(openDoor);
        }
        return null;
    }
}
