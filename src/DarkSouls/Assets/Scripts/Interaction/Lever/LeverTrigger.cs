using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    private Lever lever;
    void Start()
    {
        lever = GetComponentInParent<Lever>();
    }

    private void OnTriggerEnter(Collider other)
    {
        lever.Trigger(true);
    }

    private void OnTriggerExit(Collider other)
    {
        lever.Trigger(false);
    }
}
