using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [System.Serializable]
    public class Controller : IUIController
    {
        private KeyModifierView[] keyModifierViews;
        private Dictionary<string, KeyCode> keymap;
        private SettingsView settingsView;
        public void Init(SettingsView _settingsView)
        {
            settingsView = _settingsView;

            keyModifierViews = settingsView.keyboardSettingsView.keyModifierParent.GetComponentsInChildren<KeyModifierView>();
            settingsView.keyboardSettingsView.submitBtn.onClick.AddListener(Save);
            settingsView.keyboardSettingsView.restoreBtn.onClick.AddListener(Restore);
        }

        public void Save()
        {
            Settings.Instance.UpdateKeyMap(keyModifierViews);
            UIManager.Instance.ReturnPrev();
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

        public override void Hide()
        {
            settingsView.parent.SetActive(false);
        }

        public override void Show()
        {
            settingsView.parent.SetActive(true);
            Restore();
        }
    }
    public Controller controller;

    private SettingsView menuView;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        menuView = GetComponent<SettingsView>();
        controller.Init(menuView);
    }
}
