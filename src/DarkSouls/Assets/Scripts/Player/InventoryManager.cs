using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public class Inventory
    {
        public Dictionary<int, ItemData> datas;

        public Dictionary<int, int> count;

        private Dictionary<ItemType, int> itemType;
        private int curIndex;
        public Inventory()
        {
            curIndex = 0;
            datas = new Dictionary<int, ItemData>();
            count = new Dictionary<int, int>();
            itemType = new Dictionary<ItemType, int>();
        }

        public void AddItem(ItemData itemData, int _count)
        {
            if (itemData.allowOverlay && datas.ContainsValue(itemData))
            {
                for (int i = 0; i < datas.Count; i++)
                {
                    if (datas[i].Equals(itemData))
                    {
                        count[i] += _count;
                        break;
                    }
                }
            }
            else
            {
                datas.Add(curIndex, itemData);
                count.Add(curIndex, _count);
                itemType.Add(itemData.curItemType, curIndex);
                curIndex++;
            }
        }

        public void MinusItem(int index, int amount)
        {
            if (datas.ContainsKey(index))
            {
                count[index] += amount;
                if (count[index] <= 0)
                {
                    datas.Remove(index);
                    count.Remove(index);
                }
            }
        }

        public void RemoveItem(int index)
        {
            if (datas.ContainsKey(index))
            {
                if (count[index] == 1)
                {
                    datas.Remove(index);
                    count.Remove(index);
                }
                else if (count[index] > 1)
                {
                    count[index]--;
                }
            }
        }
    }
    public Inventory inventory;
    public Dictionary<int, ItemData> AllItemDatas
    {
        get { return inventory.datas; }
    }
    public int GetItemCount(int index)
    {
        return inventory.count[index];
    }
    public ItemData this[int index] { get { return inventory.datas.ContainsKey(index) ? inventory.datas[index] : null; } }
    public delegate void ItemAdded(ItemData itemData, int count);
    public event ItemAdded OnItemAdded;
    private ActorManager am;
    private void Start()
    {
        am = GetComponent<ActorManager>();
        inventory = new Inventory();
    }
    public void Additem(ItemData itemData, int count = 1)
    {
        inventory.AddItem(itemData, count);
        OnItemAdded.Invoke(itemData, count);
    }

    public bool UseItem(int index, int amount = 1)
    {
        if (inventory.datas.ContainsKey(index) && inventory.count[index] < 1)
            return false;
        am.ActorC.IssueTrigger("useItem");
        ItemData itemData = inventory.datas[index];
        itemData.DoEffect(am.StateM.state);
        inventory.MinusItem(index, amount);
        return true;
    }
}
