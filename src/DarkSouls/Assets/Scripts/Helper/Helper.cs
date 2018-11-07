using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Helper
{
    public static Transform DeepFind(this Transform parent, string targetName)
    {
        Transform tempTrans = null;
        foreach (Transform child in parent)
        {
            if (child.name == targetName)
                return child;
            else
            {
                tempTrans = DeepFind(child.transform, targetName);
                if (tempTrans != null)
                    return tempTrans;
            }
        }
        return tempTrans;
    }

    public static T AddComponentInChildren<T>(this GameObject parent, string targetName) where T : MonoBehaviour
    {
        T temp;
        Transform target = DeepFind(parent.transform, targetName);
        temp = target.gameObject.GetComponent<T>();
        if (temp == null)
        {
            temp = target.gameObject.AddComponent<T>();
        }
        return temp;
    }
}

