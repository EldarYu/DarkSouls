using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    [Header("Animator Field")]
    public string velocityFloat;

    [Header("Move Options")]
    public float walkSpeed;
    public float runMulti;

    public GameObject model;

    private Animator animator;
    private Vector3 movingVec;
    private Rigidbody rigid;
    private IPlayerInput pi;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        pi = GetComponent<IPlayerInput>();

        model = transform.GetChild(0).gameObject;
        animator = model.GetComponent<Animator>();

        if (animator == null || rigid == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        animator.SetFloat(velocityFloat, pi.Dmag * Mathf.Lerp(animator.GetFloat(velocityFloat), (pi.Run ? 2.0f : 1.0f), 0.5f));

        if (pi.Dmag > 0.1f)
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);

        movingVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.Run ? runMulti : 1.0f);

    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector3(movingVec.x, rigid.velocity.y, movingVec.z);
    }
}
