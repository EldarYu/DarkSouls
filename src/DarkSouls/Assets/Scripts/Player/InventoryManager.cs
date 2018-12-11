using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private ActorManager am;
    private void Start()
    {
        am = GetComponent<ActorManager>();
    }


}
