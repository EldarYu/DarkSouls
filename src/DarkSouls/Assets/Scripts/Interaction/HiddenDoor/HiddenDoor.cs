using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            this.gameObject.SetActive(false);
        }
    }
}
