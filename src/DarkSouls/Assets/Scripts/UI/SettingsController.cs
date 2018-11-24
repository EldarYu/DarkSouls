using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [System.Serializable]
    public class KeyboardSettingsController
    {
        private KeyModifierView[] keyModifierViews;
        private Dictionary<string, KeyCode> keymap;
        private SettingsView settingsView;
        public void Init(SettingsView _settingsView)
        {
            settingsView = _settingsView;
            keyModifierViews = settingsView.keyboardSettingsView.keyModifierParent.GetComponentsInChildren<KeyModifierView>();

            Restore();
            settingsView.keyboardSettingsView.submitBtn.onClick.AddListener(Save);
            settingsView.keyboardSettingsView.restoreBtn.onClick.AddListener(Restore);
        }

        public void Save()
        {
            Settings.Instance.UpdateKeyMap(keyModifierViews);
            settingsView.gameObject.SetActive(false);
        }

        public void Restore()
        {
            keymap = Settings.Instance.GetKeyMap();
            foreach (var item in keyModifierViews)
            {
                item.curKeycode = keymap[item.name];
                item.Init();
            }
        }
    }
    public KeyboardSettingsController keyboardSettingsController;

    private SettingsView menuView;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        menuView = GetComponent<SettingsView>();
        keyboardSettingsController.Init(menuView);
    }
}
