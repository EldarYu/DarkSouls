using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public float offset = 0.1f;
    public LayerMask checkLayer;
    public string isGroundMethodName;
    public string isNotGroundMethodName;

    private CapsuleCollider capcol;
    private Vector3 pointTop;
    private Vector3 pointBottom;
    private float radius;

    void Start()
    {
        capcol = transform.parent.GetComponent<CapsuleCollider>();
        radius = capcol.radius - 0.05f;
    }

    void FixedUpdate()
    {
        pointTop = transform.position + transform.up * (radius - offset);
        pointBottom = transform.position + transform.up * (capcol.height - offset) - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(pointTop, pointBottom, radius, checkLayer);
        if (outputCols.Length != 0)
            SendMessageUpwards(isGroundMethodName);
        else
            SendMessageUpwards(isNotGroundMethodName);

    }
}
