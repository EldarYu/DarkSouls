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

    public static bool CheckAngleSelf(this Transform self, Transform target, float selfAngleLimit)
    {
        Vector3 dir = target.position - self.position;

        float angle1 = Vector3.Angle(self.forward, dir);
        float angle2 = Vector3.Angle(target.forward, self.forward);

        return (angle1 < selfAngleLimit && Mathf.Abs(angle2 - 180) < selfAngleLimit);
    }

    public static bool CheckAngleTarget(this Transform self, Transform target, float targetAngleLimit)
    {
        Vector3 dir = self.position - target.position;

        float angle = Vector3.Angle(target.forward, dir);

        return angle < targetAngleLimit;
    }

    public static long SmoothDamp(long current, long target, long step)
    {
        if (current == target)
        {
            return target;
        }
        if (current < target)
        {
            current += step;

            if (current > target)
                return target;

            return current;
        }
        if (current > target)
        {
            current -= step;
            if (current < target)
                return target;
            return current;
        }
        return current;
    }
}

