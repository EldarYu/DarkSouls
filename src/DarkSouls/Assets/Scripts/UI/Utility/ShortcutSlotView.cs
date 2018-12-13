using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutSlotView : MonoBehaviour
{
    public Image curImg;
    public Text curCountText;
    public int maxCount = 3;
    public Sprite empty;
    //[HideInInspector]
    public List<ItemData> itemDatas;
    // [HideInInspector]
    public List<int> itemCounts;
    public List<int> itemIndex;
    // [HideInInspector]
    public ItemData curItemdata;
    private int curCount;
    private int curIndex;
    private int curItemIndex;
    public delegate void ItemChange(ItemData itemData);
    public event ItemChange OnItemChange;
    public delegate bool ItemUse(int itemIndex);
    public event ItemUse OnItemUse;

    public void Init(List<ItemData> _itemDatas, int _curIndex, List<int> _itemCounts, List<int> _itemIndex)
    {
        itemDatas = _itemDatas;
        itemCounts = _itemCounts;
        itemIndex = _itemIndex;
        curIndex = _curIndex;
        curItemIndex = itemIndex[curIndex];
        curItemdata = itemDatas[curIndex];
        curCount = itemCounts[curIndex];
        SetImg();
    }


    public void UseItem()
    {
        if (curItemIndex != -1)
        {
            if (OnItemUse.Invoke(curItemIndex))
            {
                itemCounts[curIndex]--;
                curCount = itemCounts[curIndex];
                SetImg();
            }
        }
    }

    public void NextItem()
    {
        curIndex++;
        if (curIndex > maxCount - 1)
            curIndex = 0;
        curItemdata = itemDatas[curIndex];
        curCount = itemCounts[curIndex];

        if (curItemdata.curItemType == ItemType.Weapon)
        {
            OnItemChange.Invoke(curItemdata);
        }
        SetImg();
    }

    public void SetImg()
    {
        if ((curItemdata.curItemType == ItemType.ForHp ||
            curItemdata.curItemType == ItemType.ForMp ||
            curItemdata.curItemType == ItemType.ForVigor) &&
            curCount == 0)
        {
            curImg.sprite = empty;
            curCountText.text = "";
        }
        else
        {
            curImg.sprite = curItemdata.img;
            if (curCount == 1)
                curCountText.text = "";
            curCountText.text = curCount.ToString();
        }
    }

    public void Clear()
    {
        itemDatas = null;
        itemCounts = null;
        curItemIndex = -1;
        curImg.sprite = null;
        curCountText.text = "";
    }
}
