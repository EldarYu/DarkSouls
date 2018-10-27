using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmFix : MonoBehaviour
{
    public string[] boolFields;
    public Vector3[] eulers;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        for (int i = 0; i < boolFields.Length; i++)
        {
            if (animator.GetBool(boolFields[i]))
            {
                Transform leftLowerArm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftLowerArm.localEulerAngles += eulers[i];
                animator.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
            }
        }
    }
}
