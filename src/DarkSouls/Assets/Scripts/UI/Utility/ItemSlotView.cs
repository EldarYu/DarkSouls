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
    public int itemCount;
    public int itemIndex;

    public delegate void OnClickHandle(ItemData itemData, int itemIndex, int itemCount);
    public event OnClickHandle OnClick;
    private UnityEngine.UI.Button btn;
    private void Start()
    {
        btn = GetComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(Enter);
    }
    public void Init(ItemData _itemData, int _itemIndex, int _itemCount = 1)
    {
        itemData = _itemData;
        nameText.text = itemData.name;
        itemImg.sprite = itemData.img;
        descriptionText.text = itemData.description;
        itemCount = _itemCount;
        this.itemIndex = _itemIndex;
        countText.text = itemCount.ToString();
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        itemImg.sprite = null;
        nameText.text = "";
        descriptionText.text = "";
        countText.text = "";
        itemCount = 0;
        itemIndex = -1;
        gameObject.SetActive(false);
    }

    public void Enter()
    {
        OnClick.Invoke(itemData, itemIndex, itemCount);
    }
}
