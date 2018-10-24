using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Dmag;
    public Vector3 Dvec;

    public bool run;

    private float Dup;
    private float Dright;
    private float velovityDup;
    private float velovityDright;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveMent(float targetDup, float targetDright)
    {
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velovityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velovityDright, 0.1f);
        SquareToCircle(ref Dup, ref Dright);
        Dmag = Mathf.Sqrt(Dup * Dup + Dright * Dright);
        Dvec = Dup * transform.forward + Dright * transform.right;
    }

    public void SquareToCircle(ref float dup, ref float dright)
    {
        dup = dup * Mathf.Sqrt(1 - (dright * dright) / 2.0f);
        dright = dright * Mathf.Sqrt(1 - (dup * dup) / 2.0f);
    }
}
