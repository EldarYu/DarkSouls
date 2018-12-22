﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private IActorManager am;
    private void Awake()
    {
        am = transform.parent.GetComponent<IActorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Weapon"))
        {
            WeaponController targetWC = other.gameObject.GetComponentInParent<WeaponController>();
            Transform self = am.ActorC.model.transform;
            Transform target = targetWC.wm.am.ActorC.model.transform;

            bool attackValid = self.CheckAngleTarget(target, 45);
            bool counterValid = self.CheckAngleSelf(target, 30);

            am.TryDoDamage(targetWC, attackValid, counterValid);
        }
    }
}
