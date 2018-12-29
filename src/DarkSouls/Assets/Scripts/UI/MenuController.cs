using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuController : MonoBehaviour
{
    [System.Serializable]
    public class Controller : IUIController
    {
        public InventoryController inventoryController;
        public SettingsController settingsController;
        public StateController stateController;
        public EquipmentController equipmentController;
        private MenuView menuView;
        public void Init(MenuView _menuView)
        {
            menuView = _menuView;
            settingsController = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<SettingsController>();
            menuView.settingsBtn.onClick.AddListener(ShowSettings);
            menuView.stateBtn.onClick.AddListener(ShowState);
            menuView.inventoryBtn.onClick.AddListener(ShowInventory);
            menuView.equipmentBtn.onClick.AddListener(ShowEquipment);
        }

        public void ShowEquipment()
        {
            UIManager.Instance.AddRecord(equipmentController.controller);
        }

        public void ShowInventory()
        {
            inventoryController.controller.curItemType = ItemType.None;
            UIManager.Instance.AddRecord(inventoryController.controller);
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
    public AudioClip enterClip;
    public AudioClip cancelClip;
    public AudioClip selectClip;
    private AudioSource audioSource;
    private GameObject lastSelected;
    private MenuView menuView;
    public void Start()
    {
        menuView = GetComponent<MenuView>();
        controller.Init(menuView);
        audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(cancelClip);
                return;
            }
        }
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        if (lastSelected != obj)
        {
            if (obj != null)
                audioSource.PlayOneShot(selectClip);
        }
        lastSelected = obj;

        if (Input.GetKeyDown(KeyCode.Return) && UIManager.Instance.Count > 0)
        {
            audioSource.PlayOneShot(enterClip);
        }
    }
}
