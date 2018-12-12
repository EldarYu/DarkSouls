using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon Data")]
public class WeaponData : ItemData
{
    public WeaponType curWeaponType;
    public GameObject obj;
    public float atk;
    public float def;
    public float ATK { get { return atk; } }
    public float DEF { get { return def; } }
}

