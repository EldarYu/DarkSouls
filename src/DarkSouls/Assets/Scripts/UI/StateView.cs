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
        public Text maxHp;
        public Text maxVigor;
        public Text maxMp;
        public Text curSouls;
        public Text requiredSouls;
        public StateModifierView strength;
        public StateModifierView stamina;
        public StateModifierView intellect;
        public UnityEngine.UI.Button saveBtn;
        public UnityEngine.UI.Button restoreBtn;
    }
    public View view;
}
