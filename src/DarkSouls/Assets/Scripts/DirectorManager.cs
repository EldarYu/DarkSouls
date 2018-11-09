using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DirectorManager : MonoBehaviour
{
    public TimelineAsset stabFront;

    private ActorManager am;
    private PlayableDirector pd;
    void Start()
    {
        am = GetComponent<ActorManager>();
        pd = GetComponent<PlayableDirector>();
    }

    public void PlayFrontStab(ActorManager attacker, ActorManager victim)
    {
        if (pd.state == PlayState.Playing)
            return;

        pd.playableAsset = Instantiate(stabFront);
        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Attacker Animation")
            {
                pd.SetGenericBinding(track, attacker.ac.GetAnimator());
            }
            else if (track.name == "Victim Animation")
            {
                pd.SetGenericBinding(track, victim.ac.GetAnimator());
            }
            else if (track.name == "Attacker ActorManager")
            {
                pd.SetGenericBinding(track, attacker);
                foreach (var clip in track.GetClips())
                {
                    LockAnimatorClip lockClip = (LockAnimatorClip)clip.asset;
                    lockClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(lockClip.am.exposedName, attacker);
                }
            }
            else if (track.name == "Victim ActorManager")
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

    public void PlayeOpenBox(ActorManager player)
    {

    }

}
