using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new UIManager();
            return instance;
        }
    }
    private Stack<IUIController> history;
    private UIManager() { history = new Stack<IUIController>(); }
    public int Count { get { return history.Count; } }

    public void ReturnPrev()
    {
        IUIController cur = history.Pop();
        cur.Hide();
        if (history.Count > 0)
        {
            IUIController prev = history.Peek();
            prev.Show();
            EventSystem.current.SetSelectedGameObject(prev.defaultSelected);
        }
    }

    public void AddRecord(IUIController next)
    {
        if (history.Count != 0)
        {
            IUIController cur = history.Peek();
            if (cur != null)
            {
                cur.Hide();
            }
        }
        next.Show();
        EventSystem.current.SetSelectedGameObject(next.defaultSelected);
        history.Push(next);
    }

    public void ClearRecord()
    {
        history.Clear();
    }
}

