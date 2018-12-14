using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShortcutModifierView : MonoBehaviour
{
    [HideInInspector]
    public int curIndex;
    public Sprite empty;
    public Image curImg;
    public Text curCountText;
    public ItemType itemType;

    public ItemData curItemData;
    public int curItemIndex;
    public int curItemCount;

    private ShortcutSlotView shortcutSlotView;

    public delegate void OnClickHandle(ShortcutModifierView shortcutModifierView, ShortcutSlotView shortcutSlotView);
    public event OnClickHandle OnClick;

    private UnityEngine.UI.Button btn;
    private void Start()
    {
        btn = GetComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(Enter);
    }

    public void Init(ItemData itemData, int itemIndex, int itemCount, int index, ShortcutSlotView _shortcutSlotView)
    {
        curItemData = itemData;
        curItemIndex = itemIndex;
        curItemCount = itemCount;
        curIndex = index;
        shortcutSlotView = _shortcutSlotView;
        SetImg();
    }

    public void SetImg()
    {
        if (curItemData == null)
        {
            Clear();
            return;
        }
        curImg.color = new Color(1, 1, 1, 1);
        if (curItemData.curItemType == ItemType.Consumable &&
            curItemData.forHp &&
            curItemCount == 0)
        {
            curImg.sprite = empty;
            curCountText.text = "";
        }
        else
        {
            curImg.sprite = curItemData.img;
            if (curItemCount == 1)
                curCountText.text = "";
            curCountText.text = curItemCount.ToString();
        }
    }

    public void Clear()
    {
        curImg.sprite = null;
        curImg.color = new Color(1, 1, 1, 0);
        curCountText.text = "";
    }

    public void Enter()
    {
        OnClick.Invoke(this, shortcutSlotView);
    }
}
