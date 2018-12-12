using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudView : MonoBehaviour
{
    public GameObject actionTip;
    [System.Serializable]
    public class StateView
    {
        public Image hp;
        public Image vigor;
        public Image mp;
        public Text souls;
    }
    public StateView stateView;
    [System.Serializable]
    public class NewItemInfoView
    {
        public GameObject parent;
        public Text name;
        public Text count;
        public Image img;
    }
    public NewItemInfoView newItemInfoView;
    [System.Serializable]
    public class DialogView
    {
        public GameObject parent;
        public Text text;
        public GameObject tip;

    }
    public DialogView dialogView;
}
