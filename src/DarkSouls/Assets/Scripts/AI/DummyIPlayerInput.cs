using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIPlayerInput : IPlayerInput
{
    public Transform target;
    public float distance;

    public void CalculateDistance(Vector3 pos)
    {
        distance = Vector3.Distance(transform.position, pos);
    }

    public virtual void Move() { }
    public virtual void Attack(Direction direction, bool isHeavy = false) { }
    public virtual void DoRun() { }
    public virtual void DoLockOn() { }
}
