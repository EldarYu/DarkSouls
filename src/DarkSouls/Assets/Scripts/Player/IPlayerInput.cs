using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerInput : MonoBehaviour
{
    public bool inputEnabled = true;

    [Header("Output Signals")]
    public float Dmag;
    public Vector3 Dvec;
    public bool Run;

    public float Jup;
    public float Jright;

    protected float Dup;
    protected float targetDup;
    protected float Dright;
    protected float targetDright;

    protected float velovityDup;
    protected float velovityDright;

    protected void SquareToCircle(ref float dup, ref float dright)
    {
        dup = dup * Mathf.Sqrt(1 - (dright * dright) / 2.0f);
        dright = dright * Mathf.Sqrt(1 - (dup * dup) / 2.0f);
    }
}
