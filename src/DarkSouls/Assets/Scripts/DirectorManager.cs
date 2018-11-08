using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorManager : MonoBehaviour
{
    private ActorManager am;
    void Start()
    {
        am = GetComponent<ActorManager>();
    }

    void Update()
    {

    }
}
