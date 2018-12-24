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
        private float velocityHp;
        private float velocityVigor;
        private float velocityMp;
        public void Tick(HudView hudView, StateManager sm)
        {
            hp = Mathf.SmoothDamp(hp, sm.state.HP, ref velocityHp, 0.1f);
            vigor = Mathf.SmoothDamp(vigor, sm.state.Vigor, ref velocityVigor, 0.1f);
            mp = Mathf.SmoothDamp(mp, sm.state.MP, ref velocityMp, 0.1f);
            souls = Helper.SmoothDamp(souls, sm.state.souls, 100);

            hudView.stateView.hp.fillAmount = hp / sm.state.MaxHP;
            hudView.stateView.vigor.fillAmount = vigor / sm.state.MaxVigor;
            hudView.stateView.mp.fillAmount = mp / sm.state.MaxMP;
            hudView.stateView.souls.text = souls.ToString();
        }
    }
    public StateController stateController;
    [System.Serializable]
    public class BossStateController
    {
        private IActorManager bossAm;
        private HudView hudView;
        private float bossHp;
        private float velocityHp;
        public void Init(HudView _hudview)
        {
            hudView = _hudview;

        }
        public void Tick()
        {
            if (bossAm != null)
            {
                if (bossAm.IsDie())
                {
                    hudView.bossStateView.parent.SetActive(false);
                    bossHp = 0;
                    hudView.bossStateView.bosName.text = "";
                    bossAm = null;
                    return;
                }
                bossHp = Mathf.SmoothDamp(bossHp, bossAm.bossHp, ref velocityHp, 0.1f);
                hudView.bossStateView.bossHp.fillAmount = bossHp / bossAm.maxBossHp;
            }

        }
        public void SetBossInfo(IActorManager _bossAm)
        {
            bossAm = _bossAm;
            hudView.bossStateView.parent.SetActive(true);
            hudView.bossStateView.bosName.text = bossAm.bossName;
        }
    }
    public BossStateController bossStateController;
    [System.Serializable]
    public class ShortcutController
    {
        public WeaponData defaultSword;
        public WeaponData defaultShield;
        private IPlayerInput pi;
        private ShortcutSlotView left;
        private ShortcutSlotView right;
        private ShortcutSlotView top;
        private ShortcutSlotView down;

        public void Init(ActorManager am, HudView hudview)
        {
            left = hudview.shortcutView.left;
            right = hudview.shortcutView.right;
            top = hudview.shortcutView.top;
            down = hudview.shortcutView.down;
            pi = am.PlayerInput;
            left.OnItemChange += am.SwitchWeapon;
            right.OnItemChange += am.SwitchWeapon;
            down.OnItemUse += am.UseItem;
            left.Init();
            right.Init();
            top.Init();
            down.Init();
            //*****************
            left.SetItem(defaultShield, 0, 1, 0);
            right.SetItem(defaultSword, 1, 1, 0);
            //*****************
        }

        public void Tick()
        {
            if (pi.ShortcutLeftSelect)
                ShortcutSelect(left);

            if (pi.ShortcutRightSelect)
                ShortcutSelect(right);

            if (pi.ShortcutTopSelect)
                ShortcutSelect(top);

            if (pi.ShortcutDownSelect)
                ShortcutSelect(down);

            if (pi.ShortcutItemUse)
                ShortcutUse(down);
        }

        public void ShortcutSelect(ShortcutSlotView tgt)
        {
            tgt.NextItem();
        }

        public void ShortcutUse(ShortcutSlotView tgt)
        {
            tgt.UseItem();
        }
    }
    public ShortcutController shortcutController;
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
        shortcutController.Init(am, hudView);
        bossStateController.Init(hudView);
    }
    void Update()
    {
        stateController.Tick(hudView, am.StateM);
        bossStateController.Tick();
        shortcutController.Tick();
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
