using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EventType
{
    OpenBox, FrontStab, LeverUp
}

public class EventCasterManager : MonoBehaviour
{
    public IActorManager am;
    public bool active;
    public EventType eventType;
    public Vector3 offset;
    public ItemData itemData;
    public int itemCount;
    private void Start()
    {
        am = GetComponentInParent<IActorManager>();

#if UNITY_EDITOR
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = true;
#endif
    }
}
