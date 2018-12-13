using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private WeaponController leftWC;
    private WeaponController rightWC;
    private Collider LeftWCol;
    private Collider rightWcol;
    [HideInInspector]
    public ActorManager am;

    private void Awake()
    {
        am = GetComponentInParent<ActorManager>();
        leftWC = gameObject.AddComponentInChildren<WeaponController>("LeftWeaponHandle");
        leftWC.wm = this;
        rightWC = gameObject.AddComponentInChildren<WeaponController>("RightWeaponHandle");
        rightWC.wm = this;
        LeftWCol = leftWC.GetComponentInChildren<Collider>();
        rightWcol = rightWC.GetComponentInChildren<Collider>();
    }

    public void ShowItem(ItemData itemData)
    {
        rightWC.ShowItem(itemData);
    }

    public void HideItem()
    {
        rightWC.HideItem();
    }

    //Animation Event
    void WeaponEnable()
    {
        if (am.ActorC.CheckAnimatorStateWithTag("attackL"))
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
}
