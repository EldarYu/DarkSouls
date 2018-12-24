using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ActiveBossMixerBehaviour : PlayableBehaviour
{

    IActorManager m_TrackBinding;
    bool m_FirstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as IActorManager;

        if (m_TrackBinding == null)
            return;

        if (!m_FirstFrameHappened)
        {
            m_FirstFrameHappened = true;
        }

        int inputCount = playable.GetInputCount ();

        float totalWeight = 0f;
        float greatestWeight = 0f;
        int currentInputs = 0;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<ActiveBossBehaviour> inputPlayable = (ScriptPlayable<ActiveBossBehaviour>)playable.GetInput(i);
            ActiveBossBehaviour input = inputPlayable.GetBehaviour ();
            
            totalWeight += inputWeight;


            if (!Mathf.Approximately (inputWeight, 0f))
                currentInputs++;
        }

    }

    public override void OnPlayableDestroy (Playable playable)
    {
        m_FirstFrameHappened = false;

        if (m_TrackBinding == null)
            return;

    }
}
