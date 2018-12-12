using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public List<EventCasterManager> overlapEcastms = new List<EventCasterManager>();
    private ActorManager am;
    void Start()
    {
        am = GetComponentInParent<ActorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EventCasterManager[] ecastms = other.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            if (!overlapEcastms.Contains(ecastm) && ecastm.active)
            {
                overlapEcastms.Add(ecastm);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventCasterManager[] ecastms = other.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            if (overlapEcastms.Contains(ecastm))
            {
                overlapEcastms.Remove(ecastm);
            }
        }
    }
}
