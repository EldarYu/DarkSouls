using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour
{
    public string checkTag = "Weapon";
    private ActorManager am;
    private void Awake()
    {
        am = transform.parent.GetComponent<ActorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(checkTag))
        {
            am.DoDamage();
        }
    }
}
