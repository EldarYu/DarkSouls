using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EventType
{
    OpenBox, FrontStab
}


public class EventCasterManager : MonoBehaviour
{
    public ActorManager am;
    public bool active;
    public EventType eventType;


    private void Start()
    {
        am = GetComponentInParent<ActorManager>();


#if UNITY_EDITOR
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = true;
#endif
    }

}
