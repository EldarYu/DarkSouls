using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsView : MonoBehaviour
{
    public GameObject parent;
    [System.Serializable]
    public class KeyboardSettingsView
    {
        public Transform keyModifierParent;
        public UnityEngine.UI.Button submitBtn;
        public UnityEngine.UI.Button restoreBtn;
    }
    public KeyboardSettingsView keyboardSettingsView;
}
