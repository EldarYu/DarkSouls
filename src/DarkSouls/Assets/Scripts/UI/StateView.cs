using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateView : MonoBehaviour
{
    public GameObject parent;
    [System.Serializable]
    public class View
    {
        public Text level;
        public Text curHp;
        public Text maxHp;
        public Text curVigor;
        public Text maxVigor;
        public Text curMp;
        public Text maxMp;
        public Text strength;
        public Text stamina;
        public Text Intellect;

    }
    public View view;

}
