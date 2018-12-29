using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Lever : MonoBehaviour
{
    [Header("0 - top pos , 1 - down pos")]
    public List<LeverManager> leverManagers;
    public Transform lever;
    public List<Vector3> leverPos;
    public Transform trigger;
    public List<Vector3> triggerPos;
    private int curLeverPosIndex;
    private int curTriggerPosIndex;
    private bool isLeverLifting = false;
    private bool isTriggerLifting = false;
    private Vector3 leverVelocity;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        curLeverPosIndex = 0;
        curTriggerPosIndex = 0;
        leverManagers[curLeverPosIndex].Init(false);
        isLeverLifting = true;
    }

    void Update()
    {
        if (isLeverLifting)
        {
            lever.localPosition = Vector3.SmoothDamp(lever.localPosition, leverPos[curLeverPosIndex], ref leverVelocity, 5.0f);
            if (Vector3.Distance(lever.localPosition, leverPos[curLeverPosIndex]) < 0.3f)
            {
                isLeverLifting = false;
                audioSource.Stop();
            }
        }

        if (isTriggerLifting)
        {
            trigger.localPosition = Vector3.Lerp(trigger.localPosition, triggerPos[curTriggerPosIndex], 0.8f);
            if (Vector3.Distance(trigger.localPosition, triggerPos[curTriggerPosIndex]) < 0.3f)
                isTriggerLifting = false;
        }
    }

    public void Trigger(bool isEnter)
    {
        if (isEnter)
        {
            curTriggerPosIndex = 1;
            isTriggerLifting = true;
            UpOrDown();
        }
        else
        {
            curTriggerPosIndex = 0;
            isTriggerLifting = true;
        }
    }

    public void UpOrDown(bool isTrigger = true)
    {
        if (isLeverLifting)
        {
            return;
        }
        else if (isTrigger)
        {
            leverManagers[curLeverPosIndex].Init(true);
            NextIndex();
            leverManagers[curLeverPosIndex].Init(false);
            isLeverLifting = true;
            audioSource.Play();
        }
        else
        {
            NextIndex();
            leverManagers[curLeverPosIndex].Init(true);
            isLeverLifting = true;
            audioSource.Play();
        }
    }

    public void NextIndex()
    {
        if (curLeverPosIndex + 1 >= leverPos.Count)
            curLeverPosIndex = 0;
        else
            curLeverPosIndex++;
    }
}
