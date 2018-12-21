using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIPlayerInput : IPlayerInput
{
    public Vector3 nextPoint;
    public float distance;

    public void CalculateDistance(Vector3 pos)
    {
        distance = Vector3.Distance(transform.position, pos);
    }

    public virtual void Move(Vector3 pos) { }
    public virtual void LockOnMove() { }
    public virtual void Attack(Direction direction, bool isHeavy = false) { }
    public virtual void DoRun(bool value = true) { }
    public virtual void DoLockOn() { }
}
