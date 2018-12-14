using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PerfabFactory
{
    private static PerfabFactory instance;
    public static PerfabFactory Instance
    {
        get
        {
            if (instance == null)
                instance = new PerfabFactory();
            return instance;
        }
    }
    public Dictionary<string, GameObject> objDatas;
    private PerfabFactory() { objDatas = new Dictionary<string, GameObject>(); }

    public GameObject GetPerfab(ItemData itemData, Transform tgtParent)
    {
        if (objDatas.ContainsKey(itemData.name))
        {
            return objDatas[itemData.name];
        }
        GameObject temp = GameObject.Instantiate(itemData.obj, tgtParent);
        objDatas.Add(itemData.name, temp);
        return temp;
    }
}

