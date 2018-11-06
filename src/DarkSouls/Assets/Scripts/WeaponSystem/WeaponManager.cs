using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string leftWeaponHandleName;
    public string rightWeaponHandleName;

    private GameObject leftWeaponHandle;
    private GameObject rightWeaponHandle;
    private Collider leftWeaponCol;
    private Collider rightWeaponCol;
    private ActorManager am;
    private void Awake()
    {
        am = GetComponentInParent<ActorManager>();
        transform.DeepFind(leftWeaponHandleName);
        leftWeaponHandle = transform.DeepFind(leftWeaponHandleName).gameObject;
        rightWeaponHandle = transform.DeepFind(rightWeaponHandleName).gameObject;
        leftWeaponCol = leftWeaponHandle.GetComponentInChildren<Collider>();
        rightWeaponCol = rightWeaponHandle.GetComponentInChildren<Collider>();
    }

    void WeaponEnable()
    {
        if (am.ac.CheckAnimatorStateWithTag("attackL"))
            leftWeaponCol.enabled = true;
        else
            rightWeaponCol.enabled = true;
    }

    void WeaponDisable()
    {
        leftWeaponCol.enabled = false;
        rightWeaponCol.enabled = false;
    }

    //来自ActorController的消息
    void OnAttackExit()
    {
        WeaponDisable();
    }
}
