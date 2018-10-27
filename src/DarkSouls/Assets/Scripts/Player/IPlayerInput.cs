using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayerInput : MonoBehaviour
{
    public bool inputEnabled = true;

    [Header("Output Signals")]
    public float Dmag;
    public Vector3 Dvec;

    public bool Run;
    public bool Jump;
    public bool Attack;

    public float Jup;
    public float Jright;

    protected bool lastJump;
    protected bool lastAttack;

    protected float Dup;
    protected float targetDup;
    protected float Dright;
    protected float targetDright;

    protected float velovityDup;
    protected float velovityDright;

    protected Vector2 SquareToCircle(Vector2 axis)
    {
        Vector2 output = Vector2.zero;
        output.x = axis.x * Mathf.Sqrt(1 - (axis.y * axis.y) / 2.0f);
        output.y = axis.y * Mathf.Sqrt(1 - (axis.x * axis.x) / 2.0f);

        return output;
    }
}
