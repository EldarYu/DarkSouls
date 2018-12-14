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
    public bool forHp;
    public bool forMp;
    public bool forVigor;
    public bool forSoul;
    public void DoEffect(State state)
    {
        switch (curItemType)
        {
            case ItemType.Weapon:
                break;
            case ItemType.Consumable:
                if (forHp)
                    state.HP += amount;
                if (forMp)
                    state.MP += amount;
                if (forVigor)
                    state.Vigor += amount;
                break;
            default:
                break;
        }
    }
}

