using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [System.Serializable]
    public class StateController
    {
        private float hp;
        private float vigor;
        private float mp;
        private long souls;

        private float velovityHp;
        private float velovityVigor;
        private float velovityMp;
        public void Tick(HudView hudView, StateManager sm)
        {
            hp = Mathf.SmoothDamp(hp, sm.state.HP, ref velovityHp, 0.1f);
            vigor = Mathf.SmoothDamp(vigor, sm.state.Vigor, ref velovityVigor, 0.1f);
            mp = Mathf.SmoothDamp(mp, sm.state.MP, ref velovityMp, 0.1f);
            souls = Helper.SmoothDamp(souls, sm.state.souls, 100);

            hudView.stateView.hp.fillAmount = hp / sm.state.MaxHP;
            hudView.stateView.vigor.fillAmount = vigor / sm.state.MaxVigor;
            hudView.stateView.mp.fillAmount = mp / sm.state.MaxMP;
            hudView.stateView.souls.text = souls.ToString();
        }
    }
    public StateController stateController;
    [System.Serializable]
    public class NewItemInfoController : IUIController
    {
        public float waitForHide;
        private ItemData curItem;
        private int curCount;
        private HudView hudView;
        private Timer timer;
        public void Init(ActorManager am, HudView _hudView)
        {
            timer = new Timer();
            hudView = _hudView;
            am.InventoryM.OnItemAdded += ShowNewItemInfo;
        }
        public void Tick(float time)
        {
            timer.Tick(time);
            if (timer.IsFinished())
            {
                UIManager.Instance.ReturnPrev();
                timer.Stop();
            }
        }

        public void ShowNewItemInfo(ItemData itemData, int count)
        {
            curCount = count;
            curItem = itemData;
            UIManager.Instance.AddRecord(this);
        }
        public override void Hide()
        {
            hudView.newItemInfoView.parent.SetActive(false);
            curCount = 0;
            curItem = null;
            hudView.newItemInfoView.name.text = "";
            hudView.newItemInfoView.count.text = "";
            hudView.newItemInfoView.img.sprite = null;
            timer.Stop();
        }
        public override void Show()
        {
            hudView.newItemInfoView.name.text = curItem.name;
            hudView.newItemInfoView.count.text = curCount.ToString();
            hudView.newItemInfoView.img.sprite = curItem.img;
            hudView.newItemInfoView.parent.SetActive(true);
            timer.Go(waitForHide);
        }
    }
    public NewItemInfoController newItemInfoController;

    [System.Serializable]
    public class DialogController : IUIController
    {
        public void Init()
        {


        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
    public DialogController dialogController;
    public ActorManager am;
    private HudView hudView;

    private void Start()
    {
        hudView = GetComponent<HudView>();
        newItemInfoController.Init(am, hudView);
    }
    void Update()
    {
        stateController.Tick(hudView, am.StateM);
        newItemInfoController.Tick(Time.deltaTime);
        SetActive(am.CanDoAction());
    }

    public void SetActive(bool active)
    {
        if (hudView.actionTip.activeSelf != active)
        {
            hudView.actionTip.SetActive(active);
        }
    }
}
