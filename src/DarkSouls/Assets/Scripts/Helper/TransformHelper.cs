using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class TransformHelper
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
}

