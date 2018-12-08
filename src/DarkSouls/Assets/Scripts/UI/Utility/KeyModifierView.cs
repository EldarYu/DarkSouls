using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyModifierView : MonoBehaviour
{
    public new string name;
    public KeyCode curKeycode;

    private UnityEngine.UI.Button button;
    private Text text;
    private bool isWaitingForKey = false;
    void Awake()
    {
        button = GetComponentInChildren<UnityEngine.UI.Button>();
        text = GetComponentInChildren<Text>();
    }

    public void Init()
    {
        text.text = Enum.GetName(typeof(KeyCode), curKeycode);
        button.onClick.AddListener(Click);
    }

    private void Update()
    {
        if (isWaitingForKey)
        {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keycode))
                {
                    if (keycode != KeyCode.Return && keycode != KeyCode.Backspace
                        && keycode != KeyCode.UpArrow && keycode != KeyCode.DownArrow
                        && keycode != KeyCode.LeftArrow && keycode != KeyCode.RightArrow)
                    {
                        this.curKeycode = keycode;
                        text.text = Enum.GetName(typeof(KeyCode), curKeycode);
                        isWaitingForKey = false;
                    }
                }
            }
        }
    }

    public void Click()
    {
        text.text = "";
        isWaitingForKey = true;
    }
}
