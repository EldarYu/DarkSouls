using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite img;
    public float amount;
    public void DoEffect(ref float tgtValue)
    {
        tgtValue += amount;
    }
}

