using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon Data")]
public class WeaponData : ItemData
{
    public WeaponType curWeaponType;
    public float atk;
    public float def;
    public GameObject otherSideObj;
    public bool IsShield { get { return curWeaponType == WeaponType.Shield ? true : false; } }
    public float ATK { get { return atk; } }
    public float DEF { get { return def; } }
}

