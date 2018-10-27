using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmFix : MonoBehaviour
{
    public Vector3 euler;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Transform leftLowerArm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        leftLowerArm.localEulerAngles += euler;
        animator.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
    }
}
