using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StateModifierView : MonoBehaviour, IUpdateSelectedHandler
{
    public Text text;
    public GameObject leftTip;
    public GameObject rightTip;
    public delegate void OnAddHandle();
    public event OnAddHandle OnAdd;
    public delegate void OnMinusHandle();
    public event OnMinusHandle OnMinus;
    private int def_value;
    private int cur_value;

    public void Init(int value)
    {
        def_value = value;
        SetText(def_value);
        leftTip.gameObject.SetActive(false);
    }

    public void SetText(int value)
    {
        cur_value = value;

        if (cur_value == def_value)
            leftTip.gameObject.SetActive(false);
        else
            leftTip.gameObject.SetActive(true);

        if (cur_value == 99)
            rightTip.gameObject.SetActive(false);
        else
            rightTip.gameObject.SetActive(true);

        text.text = cur_value.ToString();
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (cur_value > def_value)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {      
                OnMinus.Invoke();
            }
        }

        if (cur_value < 99)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            { 
                OnAdd.Invoke();
            }
        }
    }
}
