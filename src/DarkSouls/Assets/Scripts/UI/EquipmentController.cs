using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [System.Serializable]
    public class Controller : IUIController
    {
        public HudView hudView;
        public InventoryController inventoryController;
        private List<ShortcutModifierView> leftHandShortcut;
        private List<ShortcutModifierView> rightHandShortcut;
        private List<ShortcutModifierView> itemShortcut;
        private ShortcutSlotView leftHandSlot;
        private ShortcutSlotView rightHandSlot;
        private ShortcutSlotView itemSlot;
        private EquipmentView equipmentView;
        public void Init(EquipmentView _equipmentView)
        {
            equipmentView = _equipmentView;
            leftHandShortcut = equipmentView.leftHandShortcut;
            rightHandShortcut = equipmentView.rightHandShortcut;
            itemShortcut = equipmentView.itemShortcut;

            leftHandSlot = hudView.shortcutView.left;
            rightHandSlot = hudView.shortcutView.right;
            itemSlot = hudView.shortcutView.down;

            foreach (var left in leftHandShortcut)
            {
                left.OnClick += SwitchShortcut;
            }
            foreach (var right in rightHandShortcut)
            {
                right.OnClick += SwitchShortcut;
            }
            foreach (var item in itemShortcut)
            {
                item.OnClick += SwitchShortcut;
            }
        }

        public void SwitchShortcut(ShortcutModifierView shortcutModifierView, ShortcutSlotView shortcutSlotView)
        {
            inventoryController.controller.shortcutModifierView = shortcutModifierView;
            inventoryController.controller.shortcutSlotView = shortcutSlotView;
            inventoryController.controller.curItemType = shortcutModifierView.itemType;
            UIManager.Instance.AddRecord(inventoryController.controller);
        }

        public void SetShortcutModifierView()
        {
            for (int i = 0; i < leftHandSlot.itemDatas.Count; i++)
            {
                leftHandShortcut[i].Clear();
                leftHandShortcut[i].Init(leftHandSlot.itemDatas[i], leftHandSlot.itemIndex[i], leftHandSlot.itemCounts[i], i, leftHandSlot);
            }
            for (int i = 0; i < rightHandSlot.itemDatas.Count; i++)
            {
                rightHandShortcut[i].Clear();
                rightHandShortcut[i].Init(rightHandSlot.itemDatas[i], rightHandSlot.itemIndex[i], rightHandSlot.itemCounts[i], i, rightHandSlot);
            }
            for (int i = 0; i < itemSlot.itemDatas.Count; i++)
            {
                itemShortcut[i].Clear();
                itemShortcut[i].Init(itemSlot.itemDatas[i], itemSlot.itemIndex[i], itemSlot.itemCounts[i], i, itemSlot);
            }
        }

        public override void Show()
        {
            SetShortcutModifierView();
            equipmentView.parent.SetActive(true);
        }
        public override void Hide()
        {
            equipmentView.parent.SetActive(false);

        }
    }
    public Controller controller;

    private EquipmentView equipmentView;
    private void Start()
    {
        equipmentView = GetComponent<EquipmentView>();
        controller.Init(equipmentView);
    }
}
