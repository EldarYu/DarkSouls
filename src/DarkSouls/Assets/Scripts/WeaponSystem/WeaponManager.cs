using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string leftWHName;
    public string rightWHName;

    private WeaponController leftWC;
    private WeaponController rightWC;
    private Collider LeftWCol;
    private Collider rightWcol;
    [HideInInspector]
    public ActorManager am;

    private void Awake()
    {
        am = GetComponentInParent<ActorManager>();
        leftWC = gameObject.AddComponentInChildren<WeaponController>(leftWHName);
        leftWC.wm = this;
        rightWC = gameObject.AddComponentInChildren<WeaponController>(rightWHName);
        rightWC.wm = this;
        LeftWCol = leftWC.GetComponentInChildren<Collider>();
        rightWcol = rightWC.GetComponentInChildren<Collider>();
    }

    //Animation Event
    void WeaponEnable()
    {
        if (am.ac.CheckAnimatorStateWithTag("attackL"))
            LeftWCol.enabled = true;
        else
            rightWcol.enabled = true;
    }

    void WeaponDisable()
    {
        LeftWCol.enabled = false;
        rightWcol.enabled = false;
    }

    void CounterBackEnable()
    {
        am.SetCounterBackEnable(true);
    }
    void CounterBackDisable()
    {
        am.SetCounterBackEnable(false);
    }

    //来自ActorController的消息
    void OnAttackExit()
    {
        WeaponDisable();
    }
}
