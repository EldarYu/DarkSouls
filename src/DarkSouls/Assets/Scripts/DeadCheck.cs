using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ActorManager actorManager = other.GetComponent<ActorManager>();
        if (actorManager == null)
            return;
        actorManager.Die();
    }
}
