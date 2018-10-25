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

    private Transform model;
    private Animator animator;
    private Vector3 movingVec;
    private Rigidbody rigid;
    private IPlayerInput playerInput;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        playerInput = GetComponent<IPlayerInput>();

        model = transform.GetChild(0).transform;
        animator = model.GetComponent<Animator>();

        if (animator == null || rigid == null || playerInput == null)
            this.enabled = false;
    }

    private void Update()
    {
        animator.SetFloat(velocityFloat, playerInput.Dmag * Mathf.Lerp(animator.GetFloat(velocityFloat), (playerInput.Run ? 2.0f : 1.0f), 0.5f));

        if (playerInput.Dmag > 0.1f)
            model.transform.forward = Vector3.Slerp(model.transform.forward, playerInput.Dvec, 0.3f);
        movingVec = playerInput.Dmag * model.transform.forward * walkSpeed * (playerInput.Run ? runMulti : 1.0f);

    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector3(movingVec.x, rigid.velocity.y, movingVec.z);
    }
}
