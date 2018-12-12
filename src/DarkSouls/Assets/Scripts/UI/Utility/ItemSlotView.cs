using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlotView : MonoBehaviour
{
    public ItemData itemData;
    public Text nameText;
    public Text countText;
    public Image itemImg;
    public Text descriptionText;
    public int countValue;
    public int index;
    public void Init(ItemData _itemData, int _index, int count = 1)
    {
        itemData = _itemData;
        nameText.text = itemData.name;
        itemImg.sprite = itemData.img;
        descriptionText.text = itemData.description;
        countValue = count;
        index = _index;
        countText.text = countValue.ToString();
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        itemImg.sprite = null;
        nameText.text = "";
        descriptionText.text = "";
        countText.text = "";
        countValue = 0;
        index = -1;
        gameObject.SetActive(false);
    }
}
