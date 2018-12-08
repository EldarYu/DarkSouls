using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [System.Serializable]
    public class Controller : IUIController
    {
        public SettingsController settingsController;
        public StateController stateController;
        private MenuView menuView;
        public void Init(MenuView _menuView)
        {
            menuView = _menuView;
            settingsController = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<SettingsController>();
            menuView.settingsBtn.onClick.AddListener(ShowSettings);
            menuView.stateBtn.onClick.AddListener(ShowState);
        }

        public void ShowState()
        {
            UIManager.Instance.AddRecord(stateController.controller);
        }

        public void ShowSettings()
        {
            UIManager.Instance.AddRecord(settingsController.controller);
        }

        public override void Hide()
        {
            menuView.parent.gameObject.SetActive(false);
        }

        public override void Show()
        {
            menuView.parent.gameObject.SetActive(true);
        }
    }
    public Controller controller;

    private MenuView menuView;
    public void Start()
    {
        menuView = GetComponent<MenuView>();
        controller.Init(menuView);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance.Count == 0)
            {
                UIManager.Instance.AddRecord(controller);
                return;
            }

            if (UIManager.Instance.Count > 0)
            {
                UIManager.Instance.ReturnPrev();
                return;
            }

        }
    }
}
