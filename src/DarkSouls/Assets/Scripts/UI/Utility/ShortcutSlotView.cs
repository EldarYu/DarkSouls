using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutSlotView : MonoBehaviour
{
    public Image curImg;
    public Text curCountText;
    public List<ItemData> itemDatas;
    public List<int> itemCounts;
    public ItemData curItemdata;
    public int curCount;
    private int curIndex;

    public void Init(List<ItemData> _itemDatas, int _curIndex, List<int> _itemCounts)
    {
        itemDatas = _itemDatas;
        itemCounts = _itemCounts;
        curIndex = _curIndex;
        curItemdata = itemDatas[curIndex];
        curCount = itemCounts[curIndex];
    }

    public void SetImg()
    {
        curImg.sprite = curItemdata.img;
        curCountText.text = curCount.ToString();
    }

    public void Clear()
    {
        curImg.sprite = null;
        curCountText.text = "";
    }
}
