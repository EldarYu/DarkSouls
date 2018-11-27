using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [System.Serializable]
    public class StateController
    {
        public StateManager sm;

        private float hp;
        private float vigor;
        private float mp;

        private float velovityHp;
        private float velovityVigor;
        private float velovityMp;
        public void Tick(HudView hudView)
        {
            hp = Mathf.SmoothDamp(hp, sm.state.HP, ref velovityHp, 0.1f);
            vigor = Mathf.SmoothDamp(vigor, sm.state.Vigor, ref velovityVigor, 0.1f);
            mp = Mathf.SmoothDamp(mp, sm.state.MP, ref velovityMp, 0.1f);

            hudView.stateView.hp.fillAmount = hp / sm.state.maxHp;
            hudView.stateView.vigor.fillAmount = vigor / sm.state.maxVigor;
            hudView.stateView.mp.fillAmount = mp / sm.state.maxMp;
        }
    }
    public StateController stateController;

    private HudView hudView;

    private void Start()
    {
        hudView = GetComponent<HudView>();
    }
    void Update()
    {
        stateController.Tick(hudView);
    }
}
