using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public GameObject model;

    [Header("Animator Field")]
    public string velocityFloat;

    public float walkSpeed;
    public float runMulti;

    private Vector3 movingVec;

    private PlayerController playerController;
    private new Rigidbody rigidbody;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();

        rigidbody = GetComponent<Rigidbody>();

        if (playerController == null || animator == null || rigidbody == null)
            this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(velocityFloat, playerController.Dmag * Mathf.Lerp(animator.GetFloat(velocityFloat), (playerController.run ? 2.0f : 1.0f), 0.5f));

        if (playerController.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, playerController.Dvec, 0.3f);
        }

        movingVec = playerController.Dmag * model.transform.forward * walkSpeed * (playerController.run ? runMulti : 1.0f);

    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(movingVec.x, rigidbody.velocity.y, movingVec.z);
    }
}
