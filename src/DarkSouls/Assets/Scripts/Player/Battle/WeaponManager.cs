using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData defaultSword;
    public WeaponData defaultShield;
    private WeaponController leftWC;
    private WeaponController rightWC;
    private Collider LeftWCol;
    private Collider rightWcol;
    public bool leftHasWeapon;
    public bool rightHasWeapon;
    public bool isAI;
    [HideInInspector]
    public IActorManager am;
    public WeaponController RightWC { get { return rightWC; } }
    public bool LeftIsShield { get { return leftWC.IsShield; } }
    private void Awake()
    {
        am = GetComponentInParent<IActorManager>();
        if (leftHasWeapon)
        {
            leftWC = gameObject.AddComponentInChildren<WeaponController>("LeftWeaponHandle");
            leftWC.wm = this;
            leftWC.isAI = isAI;
            leftWC.Init(defaultShield.name, defaultShield, defaultShield.obj);
            LeftWCol = leftWC.GetComponentInChildren<Collider>();
        }
        if (rightHasWeapon)
        {
            rightWC = gameObject.AddComponentInChildren<WeaponController>("RightWeaponHandle");
            rightWC.wm = this;
            rightWC.isAI = isAI;
            rightWC.Init(defaultSword.name, defaultSword, defaultSword.obj);
            rightWcol = rightWC.GetComponentInChildren<Collider>();
        }
    }

    public void SwitchWeapon(ItemData itemData, Direction direction)
    {
        if (direction == Direction.Left)
        {
            WeaponData tmp = itemData as WeaponData;
            if (tmp.curWeaponType == WeaponType.Sword)
                leftWC.Init(tmp.name + "left", tmp, tmp.otherSideObj);
            else
                leftWC.Init(tmp.name, tmp, tmp.obj);
            LeftWCol = leftWC.GetComponentInChildren<Collider>();
        }

        if (direction == Direction.Right)
        {
            WeaponData tmp = itemData as WeaponData;
            rightWC.Init(tmp.name, tmp, tmp.obj);
            rightWcol = rightWC.GetComponentInChildren<Collider>();
        }
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
