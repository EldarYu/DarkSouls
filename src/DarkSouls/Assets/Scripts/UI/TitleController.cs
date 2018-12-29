using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    [System.Serializable]
    public class MenuController : IUIController
    {
        private SettingsController settingsController;
        private TitleView titleView;

        public void Init(TitleView _titleView)
        {
            titleView = _titleView;
            titleView.menuView.startBtn.onClick.AddListener(StartGame);
            titleView.menuView.settingsBtn.onClick.AddListener(ShowSettings);
            titleView.menuView.quitBtn.onClick.AddListener(QuitGame);
            settingsController = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<SettingsController>();
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private void StartGame()
        {
            titleView.loading.gameObject.SetActive(true);
            Hide();
            UIManager.Instance.ClearRecord();
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }

        private void ShowSettings()
        {
            UIManager.Instance.AddRecord(settingsController.controller);
        }

        public override void Show()
        {
            titleView.menuView.parent.SetActive(true);
        }
        public override void Hide()
        {
            titleView.menuView.parent.SetActive(false);
        }
    }
    public MenuController menuController;
    public GameObject bgm;
    public AudioClip enterClip;
    public AudioClip cancelClip;
    public AudioClip selectClip;
    private AudioSource audioSource;
    private GameObject lastSelected;
    private TitleView titleView;
    private bool first = true;
    void Awake()
    {
        titleView = GetComponent<TitleView>();
        menuController.Init(titleView);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && first)
        {
            titleView.helloScreen.gameObject.SetActive(false);
            UIManager.Instance.AddRecord(menuController);
            first = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && UIManager.Instance.Count > 1)
        {
            UIManager.Instance.ReturnPrev();
            audioSource.PlayOneShot(cancelClip);
            return;
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
