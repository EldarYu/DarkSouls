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
    public bool isAI;
    public bool IsShield { get { return weaponData.IsShield; } }
    public void Init(WeaponData _weaponData)
    {
        if (item != null)
            item.SetActive(false);
        if (weapon != null)
            weapon.SetActive(false);
        weaponData = _weaponData;

        if (isAI)
            weapon = GameObject.Instantiate(weaponData.obj, this.transform);
        else
            weapon = PerfabFactory.Instance.GetPerfab(weaponData, this.transform);

        weapon.SetActive(true);
    }

    public void ShowItem(ItemData _itemData)
    {
        if (item != null)
            item.SetActive(false);
        if (weapon != null)
            weapon.SetActive(false);

        itemData = _itemData;

        if (isAI)
            item = GameObject.Instantiate(itemData.obj, this.transform);
        else
            item = PerfabFactory.Instance.GetPerfab(itemData, this.transform);
  
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
