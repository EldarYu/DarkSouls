using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackKnightInput : DummyIPlayerInput
{
    private GameObject model;
    private void Start()
    {
        model = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }


        LockOn = false;
        RightAttack = false;
        LeftAttack = false;
        RightHeavyAttack = false;
        LeftHeavyAttack = false;
    }

    public override void FaceTarget()
    {
        transform.forward = -target.transform.GetChild(0).transform.forward;
        model.transform.forward = transform.forward;
    }

    public override void Move(Vector3 pos)
    {
        //nextPoint = pos;
        //agent.destination = nextPoint;
        //Vector3 tmp = agent.velocity.normalized;

        //Vector3 tmpDir = nextPoint - model.transform.position;
        //tmpDir.Normalize();
        //Debug.Log("x:" + tmpDir.x + "z:" + tmpDir.z);

        //UpdateDmagDvec(tmpDir.z, tmpDir.x);
    }

    private void CalculateDmagDvec()
    {
    }

    public override void LockOnMove()
    {
        //if (nextPoint != null)
        //{
        //    Vector3 tmpDir = nextPoint - transform.position;
        //    tmpDir.Normalize();
        //    Debug.Log("x:" + tmpDir.x + "z:" + tmpDir.z);

        //    UpdateDmagDvec(-tmpDir.x, -tmpDir.z);
        //    //Debug.Log("angle:" + Vector3.Angle(transform.forward, Dvec));
        //    //Debug.DrawRay(model.transform.position, Dvec * 10.0f, Color.red, 5.0f);
        //    //Dmag = Dvec.normalized.magnitude;
        //}
        //else
        //{
        //    UpdateDmagDvec(0, 0);
        //}
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

    public override void DoRun(bool value = true)
    {
        Run = value;
    }

    public override void DoLockOn()
    {
        LockOn = true;
    }
}
