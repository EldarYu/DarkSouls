using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ActiveBossBehaviour : PlayableBehaviour
{
    public IActorManager am;
    public Camera cam;
    public override void OnGraphStart(Playable playable)
    {
        cam.gameObject.SetActive(true);
    }
    public override void OnGraphStop(Playable playable)
    {
        am.StartBossBattle();
        cam.gameObject.SetActive(false);
    }
}
