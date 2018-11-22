using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIView : MonoBehaviour
{
    [System.Serializable]
    public class StateView
    {
        public Image hp;
        public Image vigor;
        public Image mp;

    }
    public StateView stateView;
}
