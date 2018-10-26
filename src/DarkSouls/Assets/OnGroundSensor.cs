using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capcol;
    public float offset = 0.1f;
    public LayerMask checkLayer;

    private Vector3 pointTop;
    private Vector3 pointBottom;
    private float radius;
    // Use this for initialization
    void Start()
    {
        radius = capcol.radius;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pointTop = transform.position + transform.up * (radius - offset);
        pointBottom = transform.position + transform.up * (capcol.height - offset) - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(pointTop, pointBottom, radius, checkLayer);
        if (outputCols.Length != 0)
            SendMessageUpwards("IsGround");
        else
            SendMessageUpwards("IsNotGround");

    }
}
