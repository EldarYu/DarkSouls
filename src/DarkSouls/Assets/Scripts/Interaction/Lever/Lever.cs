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
    public LayerMask triggerCheckLayer;
    private int curLeverPosIndex;
    private int curTriggerPosIndex;
    private bool isLeverLifting = false;
    private bool isTriggerLifting = false;
    private Vector3 velocity;
    // Use this for initialization
    void Start()
    {
        curLeverPosIndex = 0;
        curTriggerPosIndex = 0;
        leverManagers[curLeverPosIndex].Init(false);
        isLeverLifting = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrigger();
        if (isLeverLifting)
        {
            //lever.transform.DOLocalMove(leverPos[curLeverPosIndex], 5.0f);
            lever.localPosition = Vector3.SmoothDamp(lever.localPosition, leverPos[curLeverPosIndex], ref velocity, 5.0f);
            if (Vector3.Distance(lever.localPosition, leverPos[curLeverPosIndex]) < 0.3f)
                isLeverLifting = false;
        }

        if (isTriggerLifting)
        {
            trigger.localPosition = Vector3.Lerp(trigger.localPosition, triggerPos[curTriggerPosIndex], 0.8f);
            if (Vector3.Distance(trigger.localPosition, triggerPos[curTriggerPosIndex]) < 0.3f)
                isTriggerLifting = false;
        }
    }

    public void CheckTrigger()
    {
        isTriggerLifting = true;

#if UNITY_EDITOR
        Debug.DrawRay(trigger.position, Vector3.up * 0.5f, Color.red);
#endif

        if (Physics.Raycast(trigger.position, Vector3.up, 0.5f, triggerCheckLayer))
        {
            curTriggerPosIndex = 1;
            UpOrDown();
        }
        else
        {
            curTriggerPosIndex = 0;
        }
    }

    public void UpOrDown()
    {
        if (isLeverLifting)
        {
            return;
        }
        else
        {
            isLeverLifting = true;
            NextIndex();
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
