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
    [HideInInspector]
    public List<ItemData> itemDatas;
    [HideInInspector]
    public List<int> itemCounts;
    [HideInInspector]
    public List<int> itemIndex;
    [HideInInspector]
    public ItemData curItemData;
    private int curIndex;
    private int curItemIndex;
    private int curItemCount;
    public delegate void OnItemChangeHandle(ItemData itemData, Direction curDir);
    public event OnItemChangeHandle OnItemChange;
    public delegate bool OnItemUseHandle(int itemIndex, int amount);
    public event OnItemUseHandle OnItemUse;

    public void Init()
    {
        itemDatas = new List<ItemData>(maxCount) { null, null, null };
        itemCounts = new List<int>(maxCount) { -1, -1, -1 };
        itemIndex = new List<int>(maxCount) { -1, -1, -1 };
        curIndex = 0;
        curItemIndex = itemIndex[curIndex];
        curItemData = itemDatas[curIndex];
        curItemCount = itemCounts[curIndex];
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
            curItemData = itemDatas[curIndex];
            curItemCount = itemCounts[curIndex];
            if (OnItemChange != null)
                OnItemChange.Invoke(curItemData, curDir);
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
                curItemCount = itemCounts[curIndex];
                if (curItemCount <= 0)
                {
                    itemDatas[curIndex] = null;
                    itemCounts[curIndex] = -1;
                    itemIndex[curIndex] = -1;
                    curItemIndex = itemIndex[curIndex];
                    curItemData = itemDatas[curIndex];
                    curItemCount = itemCounts[curIndex];
                }
                SetImg();
            }
        }
    }

    public void NextItem()
    {
        curIndex++;
        if (curIndex > maxCount - 1)
            curIndex = 0;
        curItemData = itemDatas[curIndex];
        curItemCount = itemCounts[curIndex];

        if (curDir == Direction.Left || curDir == Direction.Right)
        {
            OnItemChange.Invoke(curItemData, curDir);
        }
        SetImg();
    }

    private void SetImg()
    {
        if (curItemData == null)
        {
            Clear();
            return;
        }
        curImg.color = new Color(1, 1, 1, 1);
        curImg.sprite = curItemData.img;
        if (curItemCount == 1)
            curCountText.text = "";
        curCountText.text = curItemCount.ToString();
    }

    public void Clear()
    {
        curItemIndex = -1;
        curImg.sprite = null;
        curImg.color = new Color(1, 1, 1, 0);
        curCountText.text = "";
    }
}
