using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    public ItemType curItemType;
    public string itemName;
    public string description;
    public Sprite img;
    public float amount;
    public GameObject obj;
    public bool allowOverlay;
    public void DoEffect(State state)
    {
        switch (curItemType)
        {
            case ItemType.Weapon:
                break;
            case ItemType.ForHp:
                state.HP += amount;
                break;
            case ItemType.ForMp:
                state.MP += amount;
                break;
            case ItemType.ForVigor:
                state.Vigor += amount;
                break;
            default:
                break;
        }
    }
}

