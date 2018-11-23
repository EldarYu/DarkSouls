using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Settings
{
    private string pathName = "settings.json";

    private JSONObject keyboardMap;
    private JSONObject settings;

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
        string temp = SettingReader();
        if (temp != null)
        {
            settings = new JSONObject(SettingReader());
            keyboardMap = settings["KeyboardMap"];
        }

        //KeyMap keyMap = new KeyMap();
        //SetKeyMap(keyMap);
        //keyboardMap["run"].str = "sppce";
        //SaveData();
    }

    public KeyMap GetKeyMap()
    {
        try
        {
            KeyMap keyMap = new KeyMap();
            keyMap.runKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["run"].str);
            keyMap.forwardKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["forward"].str);
            keyMap.backwardKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["backward"].str);
            keyMap.leftKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["left"].str);
            keyMap.rightKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["right"].str);
            keyMap.upArrowKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["upArrow"].str);
            keyMap.downArrowKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["downArrow"].str);
            keyMap.leftArrowKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["leftArrow"].str);
            keyMap.rightArrowKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["rightArrow"].str);
            keyMap.lockKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["lock"].str);
            keyMap.actionKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["action"].str);
            keyMap.leftAtkKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["leftAttack"].str);
            keyMap.leftHAtkKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["leftHeavyAttack"].str);
            keyMap.rightAtkKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["rightAttack"].str);
            keyMap.rightHAtkKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["rightHeavyAttack"].str);
            keyMap.itemUpKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["itemUp"].str);
            keyMap.itemDownKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["itemDown"].str);
            keyMap.itemLeftKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["itemLeft"].str);
            keyMap.itemRightKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardMap["itemRight"].str);

            return keyMap;
        }
        catch (Exception)
        {
            return new KeyMap();
        }
    }

    public void SetKeyMap(KeyMap keyMap)
    {
        keyboardMap["run"].str = Enum.GetName(typeof(KeyCode), keyMap.runKey);
        keyboardMap["forward"].str = Enum.GetName(typeof(KeyCode), keyMap.forwardKey);
        keyboardMap["backward"].str = Enum.GetName(typeof(KeyCode), keyMap.backwardKey);
        keyboardMap["left"].str = Enum.GetName(typeof(KeyCode), keyMap.leftKey);
        keyboardMap["right"].str = Enum.GetName(typeof(KeyCode), keyMap.rightKey);
        keyboardMap["upArrow"].str = Enum.GetName(typeof(KeyCode), keyMap.upArrowKey);
        keyboardMap["downArrow"].str = Enum.GetName(typeof(KeyCode), keyMap.downArrowKey);
        keyboardMap["leftArrow"].str = Enum.GetName(typeof(KeyCode), keyMap.leftArrowKey);
        keyboardMap["rightArrow"].str = Enum.GetName(typeof(KeyCode), keyMap.rightArrowKey);
        keyboardMap["lock"].str = Enum.GetName(typeof(KeyCode), keyMap.lockKey);
        keyboardMap["action"].str = Enum.GetName(typeof(KeyCode), keyMap.actionKey);
        keyboardMap["leftAttack"].str = Enum.GetName(typeof(KeyCode), keyMap.leftAtkKey);
        keyboardMap["leftHeavyAttack"].str = Enum.GetName(typeof(KeyCode), keyMap.leftHAtkKey);
        keyboardMap["rightAttack"].str = Enum.GetName(typeof(KeyCode), keyMap.rightAtkKey);
        keyboardMap["rightHeavyAttack"].str = Enum.GetName(typeof(KeyCode), keyMap.rightHAtkKey);
        keyboardMap["itemUp"].str = Enum.GetName(typeof(KeyCode), keyMap.itemUpKey);
        keyboardMap["itemDown"].str = Enum.GetName(typeof(KeyCode), keyMap.itemDownKey);
        keyboardMap["itemLeft"].str = Enum.GetName(typeof(KeyCode), keyMap.itemLeftKey);
        keyboardMap["itemRight"].str = Enum.GetName(typeof(KeyCode), keyMap.itemRightKey);
    }

    private bool SaveData()
    {
        return SettingWriter(settings.ToString());
    }

    private string SettingReader()
    {
        string temp = null;
        try
        {
            using (StreamReader sr = new StreamReader(pathName, Encoding.UTF8))
            {
                temp = sr.ReadToEnd();
            }
            return temp;
        }
        catch (Exception)
        {
            return temp;
        }
    }

    private bool SettingWriter(string data)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(pathName, false, Encoding.UTF8))
            {
                sw.Write(data);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

