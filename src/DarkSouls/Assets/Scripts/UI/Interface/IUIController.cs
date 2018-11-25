using UnityEngine;
[System.Serializable]
public class IUIController
{
    public GameObject defaultSelected;
    public virtual void Show() { }
    public virtual void Hide() { }
}
