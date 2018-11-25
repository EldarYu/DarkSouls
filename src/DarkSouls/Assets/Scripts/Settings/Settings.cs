using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class KeyMap
{
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode runKey = KeyCode.Space;
    public KeyCode upArrowKey = KeyCode.I;
    public KeyCode downArrowKey = KeyCode.K;
    public KeyCode leftArrowKey = KeyCode.J;
    public KeyCode rightArrowKey = KeyCode.L;
    public KeyCode rightAtkKey = KeyCode.H;
    public KeyCode leftAtkKey = KeyCode.LeftShift;
    public KeyCode rightHAtkKey = KeyCode.U;
    public KeyCode leftHAtkKey = KeyCode.Tab;
    public KeyCode lockKey = KeyCode.V;
    public KeyCode actionKey = KeyCode.F;
    public KeyCode itemUpKey = KeyCode.R;
    public KeyCode itemDownKey = KeyCode.B;
    public KeyCode itemLeftKey = KeyCode.Q;
    public KeyCode itemRightKey = KeyCode.E;
}
[Serializable]
public class SettingsData
{
    public KeyMap keyMap = new KeyMap();
}

public class Settings
{
    public KeyMap keyMap { get { return settingsData.keyMap; } }
    private SettingsData settingsData;

    private string path = "settings.dat";

    private static Settings instance;
    public static Settings Instance
    {
        get
        {
            if (instance == null)
                instance = new Settings();
            return instance;
        }
    }

    private Settings()
    {
        settingsData = DeSerialize<SettingsData>(path);
    }

    public Dictionary<string, KeyCode> GetKeyMap()
    {
        return new Dictionary<string, KeyCode>()
        {
            { "run",keyMap.runKey},
            { "forward",keyMap.forwardKey },
            {"backward",keyMap.backwardKey },
            { "left",keyMap.leftKey},
            { "right",keyMap.rightKey},
            { "upArrow",keyMap.upArrowKey},
            { "downArrow",keyMap.downArrowKey},
            { "leftArrow",keyMap.leftArrowKey},
            { "rightArrow",keyMap.rightArrowKey},
            { "lock",keyMap.lockKey},
            { "action",keyMap.actionKey},
            { "leftAttack",keyMap.leftAtkKey},
            { "rightAttack",keyMap.rightAtkKey},
            { "leftHeavyAttack",keyMap.leftHAtkKey},
            { "rightHeavyAttack",keyMap.rightHAtkKey},
            { "itemUp",keyMap.itemUpKey},
            { "itemDown",keyMap.itemDownKey},
            { "itemLeft",keyMap.itemLeftKey},
            { "itemRight",keyMap.itemRightKey}
        };
    }

    public void UpdateKeyMap(KeyModifierView[] keyModifierView)
    {
        foreach (var item in keyModifierView)
        {
            switch (item.name)
            {
                case "run":
                    settingsData.keyMap.runKey = item.curKeycode;
                    break;
                case "forward":
                    settingsData.keyMap.forwardKey = item.curKeycode;
                    break;
                case "backward":
                    settingsData.keyMap.backwardKey = item.curKeycode;
                    break;
                case "left":
                    settingsData.keyMap.leftKey = item.curKeycode;
                    break;
                case "right":
                    settingsData.keyMap.rightKey = item.curKeycode;
                    break;
                case "upArrow":
                    settingsData.keyMap.upArrowKey = item.curKeycode;
                    break;
                case "downArrow":
                    settingsData.keyMap.downArrowKey = item.curKeycode;
                    break;
                case "leftArrow":
                    settingsData.keyMap.leftArrowKey = item.curKeycode;
                    break;
                case "rightArrow":
                    settingsData.keyMap.rightArrowKey = item.curKeycode;
                    break;
                case "lock":
                    settingsData.keyMap.lockKey = item.curKeycode;
                    break;
                case "action":
                    settingsData.keyMap.actionKey = item.curKeycode;
                    break;
                case "leftAttack":
                    settingsData.keyMap.leftAtkKey = item.curKeycode;
                    break;
                case "rightAttack":
                    settingsData.keyMap.rightAtkKey = item.curKeycode;
                    break;
                case "leftHeavyAttack":
                    settingsData.keyMap.leftHAtkKey = item.curKeycode;
                    break;
                case "rightHeavyAttack":
                    settingsData.keyMap.rightHAtkKey = item.curKeycode;
                    break;
                case "itemUp":
                    settingsData.keyMap.itemUpKey = item.curKeycode;
                    break;
                case "itemDown":
                    settingsData.keyMap.itemDownKey = item.curKeycode;
                    break;
                case "itemLeft":
                    settingsData.keyMap.itemLeftKey = item.curKeycode;
                    break;
                case "itemRight":
                    settingsData.keyMap.itemRightKey = item.curKeycode;
                    break;
            }
        }
        Serialize(settingsData, path);
    }

    public bool Serialize<T>(T obj, string path)
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public T DeSerialize<T>(string path) where T : new()
    {
        T temp = new T();
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                temp = (T)bf.Deserialize(fs);
            }
            return temp;
        }
        catch (Exception)
        {
            return temp;
        }
    }
}

