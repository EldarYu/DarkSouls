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

    public GameObject GetPerfab(string name, GameObject obj, Transform tgtParent)
    {
        if (objDatas.ContainsKey(name))
        {
            return objDatas[name];
        }
        GameObject temp = GameObject.Instantiate(obj, tgtParent);
        objDatas.Add(name, temp);
        return temp;
    }
}

