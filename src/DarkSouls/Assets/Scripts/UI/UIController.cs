using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [System.Serializable]
    public class StateController
    {
        public StateManager playerState;

        private float hp;
        private float vigor;
        private float mp;

        private float velovityHp;
        private float velovityVigor;

        public void Tick(UIView uIManager)
        {
            hp = Mathf.SmoothDamp(hp, playerState.state.hp, ref velovityHp, 0.1f);
            vigor = Mathf.SmoothDamp(vigor, playerState.state.vigor, ref velovityVigor, 0.1f);

            uIManager.stateView.hp.fillAmount = hp / playerState.state.maxHp;
            uIManager.stateView.vigor.fillAmount = vigor / playerState.state.maxVigor;
        }
    }
    public StateController stateController;

    private UIView uIManager;

    private void Start()
    {
        uIManager = GetComponent<UIView>();
    }
    void Update()
    {
        stateController.Tick(uIManager);
    }
}
