using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightInput : DummyIPlayerInput
{
    private void Update()
    {
        if (target != null)
            CalculateDistance(target.position);

        LockOn = false;
        RightAttack = false;
        LeftAttack = false;
        RightHeavyAttack = false;
        LeftHeavyAttack = false;
    }

    public override void Move()
    {
        if (target != null)
        {
            Dvec = target.transform.position - transform.position;
            Dmag = Dvec.normalized.magnitude;
        }
        else
        {
            Dvec = Vector3.zero;
            Dmag = 0;
        }
    }

    public override void Attack(Direction direction, bool isHeavy = false)
    {
        switch (direction)
        {
            case Direction.Left:
                if (!isHeavy)
                    LeftAttack = true;
                else
                    LeftHeavyAttack = true;
                break;
            case Direction.Right:
                if (!isHeavy)
                    RightAttack = true;
                else
                    RightHeavyAttack = true;
                break;
            default:
                break;
        }
    }

    public override void DoRun()
    {
        Run = true;
    }

    public override void DoLockOn()
    {
        LockOn = true;
    }
}
