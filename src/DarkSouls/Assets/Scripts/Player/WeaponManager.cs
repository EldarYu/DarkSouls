using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Collider weaponCol;
    void WeaponEnable()
    {
        weaponCol.enabled = true;
    }

    void WeaponDisable()
    {
        weaponCol.enabled = false;
    }
}
