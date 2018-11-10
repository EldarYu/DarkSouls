using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DirectorManager : MonoBehaviour
{
    public TimelineAsset stabFront;
    public TimelineAsset openBox;

    private ActorManager am;
    private PlayableDirector pd;
    void Start()
    {
        am = GetComponent<ActorManager>();
        pd = GetComponent<PlayableDirector>();
    }

    public void PlayFrontStab(IActorManager attacker, IActorManager victim)
    {
        if (pd.state == PlayState.Playing)
            return;

        pd.playableAsset = Instantiate(stabFront);
        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Attacker Animation")
            {
                pd.SetGenericBinding(track, attacker.GetAnimator());
            }
            else if (track.name == "Victim Animation")
            {
                pd.SetGenericBinding(track, victim.GetAnimator());
            }
            else if (track.name == "Attacker Script")
            {
                pd.SetGenericBinding(track, attacker);
                foreach (var clip in track.GetClips())
                {
                    LockAnimatorClip lockClip = (LockAnimatorClip)clip.asset;
                    lockClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(lockClip.am.exposedName, attacker);
                }
            }
            else if (track.name == "Victim Script")
            {
                pd.SetGenericBinding(track, victim);
                foreach (var clip in track.GetClips())
                {
                    LockAnimatorClip lockClip = (LockAnimatorClip)clip.asset;
                    lockClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(lockClip.am.exposedName, victim);
                }
            }
        }
        pd.Evaluate();
        pd.Play();
    }

    public void PlayeOpenBox(IActorManager player, IActorManager box)
    {
        if (pd.state == PlayState.Playing)
            return;

        pd.playableAsset = Instantiate(openBox);
        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Player Animation")
            {
                pd.SetGenericBinding(track, player.GetAnimator());
            }
            else if (track.name == "Box Animation")
            {
                pd.SetGenericBinding(track, box.GetAnimator());
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
            else if (track.name == "Box Script")
            {
                pd.SetGenericBinding(track, box);
                foreach (var clip in track.GetClips())
                {
                    LockAnimatorClip lockClip = (LockAnimatorClip)clip.asset;
                    lockClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(lockClip.am.exposedName, box);
                }
            }
        }
        pd.Evaluate();
        pd.Play();
    }
}
