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
    public class DialogController : IUIController
    {
        public void Init()
        {


        }


    }
    public DialogController dialogController;
    public ActorManager am;
    private HudView hudView;

    private void Start()
    {
        hudView = GetComponent<HudView>();
    }
    void Update()
    {
        stateController.Tick(hudView, am.sm);
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
