using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public class Inventory
    {
        public Dictionary<int, ItemData> datas;
        public Dictionary<int, int> count;
        public Dictionary<ItemType, List<int>> itemType;
        private int curIndex;
        public Inventory()
        {
            curIndex = 0;
            datas = new Dictionary<int, ItemData>();
            count = new Dictionary<int, int>();
            itemType = new Dictionary<ItemType, List<int>>();
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
                if (!itemType.ContainsKey(itemData.curItemType))
                {
                    itemType.Add(itemData.curItemType, new List<int>());
                }
                itemType[itemData.curItemType].Add(curIndex);
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
                    RemoveItem(index);
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
                    itemType[datas[index].curItemType].Remove(index);
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
    public Dictionary<int, ItemData> this[ItemType itemType]
    {
        get
        {
            Dictionary<int, ItemData> temp = new Dictionary<int, ItemData>();
            if (inventory.itemType.ContainsKey(itemType))
            {
                foreach (var index in inventory.itemType[itemType])
                {
                    temp.Add(index, inventory.datas[index]);
                }
            }
            return temp;
        }
    }
    public delegate void OnItemAddedHandle(ItemData itemData, int count);
    public event OnItemAddedHandle OnItemAdded;
    public WeaponData defaultSword;
    public WeaponData defaultShield;

    private ActorManager am;
    private void Start()
    {
        am = GetComponent<ActorManager>();
        inventory = new Inventory();
        //*****************
        inventory.AddItem(defaultShield, 1);
        inventory.AddItem(defaultSword, 1);

        inventory.AddItem(defaultSword, 1);
        inventory.AddItem(defaultSword, 1);
        inventory.AddItem(defaultSword, 1);
        inventory.AddItem(defaultSword, 1);
        inventory.AddItem(defaultSword, 1);
        inventory.AddItem(defaultSword, 1);
        inventory.AddItem(defaultSword, 1);
        inventory.AddItem(defaultSword, 1);
        inventory.AddItem(defaultSword, 1);
        //*****************
    }
    public void Additem(ItemData itemData, int count = 1)
    {
        inventory.AddItem(itemData, count);
        OnItemAdded.Invoke(itemData, count);
    }

    public bool UseItem(int index, int amount = -1)
    {
        if (!inventory.datas.ContainsKey(index) || inventory.count[index] < 1)
            return false;
        //print(am.ActorC.CheckAnimatorStateWithTag("useItem"));
        ItemData itemData = inventory.datas[index];
        if (itemData.curItemType == ItemType.Consumable)
        {
            if (itemData.forHp || itemData.forMp || itemData.forVigor)
            {
                am.ActorC.IssueTrigger("useItem");
            }

            if (itemData.forSoul)
            {

            }
        }
        itemData.DoEffect(am.StateM.state);
        inventory.MinusItem(index, amount);
        return true;
    }
}
