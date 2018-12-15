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
    public bool isAI;
    [HideInInspector]
    public ActorManager am;
    public bool LeftIsShield { get { return leftWC.IsShield; } }
    private void Awake()
    {
        am = GetComponentInParent<ActorManager>();
        leftWC = gameObject.AddComponentInChildren<WeaponController>("LeftWeaponHandle");
        leftWC.wm = this;
        rightWC = gameObject.AddComponentInChildren<WeaponController>("RightWeaponHandle");
        rightWC.wm = this;
        leftWC.isAI = isAI;
        rightWC.isAI = isAI;
        //*****************
        leftWC.Init(defaultShield);
        rightWC.Init(defaultSword);
        //*****************
        LeftWCol = leftWC.GetComponentInChildren<Collider>();
        rightWcol = rightWC.GetComponentInChildren<Collider>();
    }

    public void SwitchWeapon(ItemData itemData, Direction direction)
    {
        if (direction == Direction.Left)
        {
            leftWC.Init(itemData as WeaponData);
            LeftWCol = leftWC.GetComponentInChildren<Collider>();
        }

        if (direction == Direction.Right)
        {
            rightWC.Init(itemData as WeaponData);
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
