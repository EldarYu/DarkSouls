using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [HideInInspector]
    public WeaponManager wm;
    public GameObject weapon;
    public WeaponData weaponData;
    public ItemData itemData;
    public GameObject item;
    public void Init(WeaponData _weaponData)
    {
        if (item != null)
            item.SetActive(false);
        if (weapon != null)
            weapon.SetActive(false);
        weaponData = _weaponData;
        weapon = PerfabFactory.Instance.GetPerfab(weaponData);
        weapon.transform.parent = this.transform;
        weapon.SetActive(true);
    }

    public void ShowItem(ItemData _itemData)
    {
        if (item != null)
            item.SetActive(false);
        if (weapon != null)
            weapon.SetActive(false);

        itemData = _itemData;
        item = PerfabFactory.Instance.GetPerfab(itemData);
        item.transform.parent = this.transform;
        item.SetActive(true);
    }

    public void HideItem()
    {
        if (item != null)
            item.SetActive(false);
        if (weapon != null)
            weapon.SetActive(true);
    }
}
