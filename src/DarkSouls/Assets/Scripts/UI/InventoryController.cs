using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [System.Serializable]
    public class Controller : IUIController
    {
        private InventoryManager im;
        private ItemSlotView[] itemSlotViews;
        private List<ItemSlotView> itemslotPool;
        private InventoryView inventoryView;
        public void Init(ActorManager am, InventoryView _inventoryView)
        {
            im = am.InventoryM;
            inventoryView = _inventoryView;
            itemSlotViews = inventoryView.itemSlotParent.GetComponentsInChildren<ItemSlotView>();
            itemslotPool = new List<ItemSlotView>();
        }

        public void SetItemSlotViews(Dictionary<int, ItemData> datas)
        {
            int count = datas.Count;
            if (count == 0)
                return;

            if (itemslotPool.Count == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    ItemSlotView temp = Instantiate(inventoryView.itemSlotPrefab, inventoryView.itemSlotParent.transform).GetComponent<ItemSlotView>();
                    itemslotPool.Add(temp);
                }
            }
            else if (itemslotPool.Count < count)
            {
                int amount = count - itemslotPool.Count;
                for (int i = 0; i < amount; i++)
                {
                    ItemSlotView temp = Instantiate(inventoryView.itemSlotPrefab, inventoryView.itemSlotParent.transform).GetComponent<ItemSlotView>();
                    itemslotPool.Add(temp);
                }
            }

            foreach (var item in itemslotPool)
            {
                item.Clear();
            }

            int tmpIndex = 0;
            defaultSelected = itemslotPool[0].gameObject;
            foreach (var item in datas)
            {
                itemslotPool[tmpIndex].Init(item.Value, item.Key, im.GetItemCount(item.Key));
                tmpIndex++;
            }
        }

        public override void Hide()
        {
            inventoryView.parent.SetActive(false);
        }
        public override void Show()
        {
            SetItemSlotViews(im.AllItemDatas);
            inventoryView.parent.SetActive(true);
        }
    }
    public Controller controller;

    public ActorManager am;
    private InventoryView inventoryView;
    private void Start()
    {
        inventoryView = GetComponent<InventoryView>();
        controller.Init(am, inventoryView);
    }
}
