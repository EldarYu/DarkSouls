using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutSlotView : MonoBehaviour
{
    public Direction curDir;
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
    public delegate void ItemChange(ItemData itemData, Direction curDir);
    public event ItemChange OnItemChange;
    public delegate bool ItemUse(int itemIndex, int amount);
    public event ItemUse OnItemUse;

    public void Init()
    {
        itemDatas = new List<ItemData>(maxCount) { null, null, null };
        itemCounts = new List<int>(maxCount) { -1, -1, -1 };
        itemIndex = new List<int>(maxCount) { 0, 0, 0 };
        curIndex = 0;
        curItemIndex = itemIndex[curIndex];
        curItemdata = itemDatas[curIndex];
        curCount = itemCounts[curIndex];
        SetImg();
    }

    public void SetItem(ItemData _itemData, int _itemIndex, int _itemCount, int _curindex)
    {
        itemDatas[_curindex] = _itemData;
        itemCounts[_curindex] = _itemCount;
        itemIndex[_curindex] = _itemIndex;
        if (_curindex == curIndex)
        {
            curItemIndex = itemIndex[curIndex];
            curItemdata = itemDatas[curIndex];
            curCount = itemCounts[curIndex];
            SetImg();
        }
    }

    public void UseItem()
    {
        if (curDir != Direction.Down)
            return;
        if (curItemIndex != -1)
        {
            if (OnItemUse.Invoke(curItemIndex, -1))
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

        if (curDir == Direction.Left || curDir == Direction.Right)
        {
            OnItemChange.Invoke(curItemdata, curDir);
        }
        SetImg();
    }

    private void SetImg()
    {
        if (curItemdata == null)
        {
            Clear();
            return;
        }
        curImg.color = new Color(1, 1, 1, 1);
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
        curItemIndex = -1;
        curImg.sprite = null;
        curImg.color = new Color(1, 1, 1, 0);
        curCountText.text = "";
    }
}
