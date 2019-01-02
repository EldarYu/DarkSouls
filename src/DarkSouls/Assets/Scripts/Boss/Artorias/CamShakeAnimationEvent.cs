using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeAnimationEvent : MonoBehaviour
{
    private IActorManager am;

    private void Start()
    {
        am = GetComponentInParent<IActorManager>();
    }

    public void CamShakeOnce()
    {
        am.CamShakeOnce();
    }
}
