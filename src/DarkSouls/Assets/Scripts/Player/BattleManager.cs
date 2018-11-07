﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private ActorManager am;
    private void Awake()
    {
        am = transform.parent.GetComponent<ActorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            am.TryDoDamage(other.gameObject.GetComponentInParent<WeaponController>());
        }
    }
}
