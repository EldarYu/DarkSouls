using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleView : MonoBehaviour
{
    public GameObject helloScreen;
    public GameObject loading;

    [System.Serializable]
    public class MenuView
    {
        public GameObject parent;
        public UnityEngine.UI.Button startBtn;
        public UnityEngine.UI.Button settingsBtn;
        public UnityEngine.UI.Button quitBtn;
    }
    public MenuView menuView;
}
