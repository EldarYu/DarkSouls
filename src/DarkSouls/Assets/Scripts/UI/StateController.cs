using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [System.Serializable]
    public class Controller : IUIController
    {
        private StateManager sm;
        private StateView stateView;
        private State playerState;
        public void Init(ActorManager am, StateView _stateView)
        {
            sm = am.StateM;
            stateView = _stateView;

            stateView.view.strength.OnAdd += AddStrength;
            stateView.view.strength.OnMinus += MinusStrength;
            stateView.view.stamina.OnAdd += AddStamina;
            stateView.view.stamina.OnMinus += MinusStamina;
            stateView.view.intellect.OnAdd += AddIntellect;
            stateView.view.intellect.OnMinus += MinusIntellect;
            stateView.view.saveBtn.onClick.AddListener(Save);
            stateView.view.restoreBtn.onClick.AddListener(Restore);
        }

        public void InitStatePanel()
        {
            stateView.view.level.text = playerState.Level.ToString();
            stateView.view.maxHp.text = ((int)playerState.MaxHP).ToString();
            stateView.view.maxVigor.text = ((int)playerState.MaxVigor).ToString();
            stateView.view.maxMp.text = ((int)playerState.MaxMP).ToString();
            stateView.view.curSouls.text = playerState.souls.ToString();
            stateView.view.requiredSouls.text = playerState.RequiredForUpgrade.ToString();
            stateView.view.strength.Init(playerState.Strength);
            stateView.view.stamina.Init(playerState.Stamina);
            stateView.view.intellect.Init(playerState.Intellect);
        }

        public void SetStatePanel()
        {
            stateView.view.level.text = playerState.Level.ToString();
            stateView.view.maxHp.text = ((int)playerState.MaxHP).ToString();
            stateView.view.maxVigor.text = ((int)playerState.MaxVigor).ToString();
            stateView.view.maxMp.text = ((int)playerState.MaxMP).ToString();
            stateView.view.curSouls.text = playerState.souls.ToString();
            stateView.view.requiredSouls.text = playerState.RequiredForUpgrade.ToString();
            stateView.view.strength.SetText(playerState.Strength);
            stateView.view.stamina.SetText(playerState.Stamina);
            stateView.view.intellect.SetText(playerState.Intellect);
        }

        public void AddStrength()
        {
            playerState.CountStrength(1);
            playerState.Init();
            SetStatePanel();
        }
        public void MinusStrength()
        {
            playerState.CountStrength(-1);
            playerState.Init();
            SetStatePanel();
        }
        public void AddStamina()
        {
            playerState.CountStamina(1);
            playerState.Init();
            SetStatePanel();
        }
        public void MinusStamina()
        {
            playerState.CountStamina(-1);
            playerState.Init();
            SetStatePanel();
        }
        public void AddIntellect()
        {
            playerState.CountIntellect(1);
            playerState.Init();
            SetStatePanel();
        }
        public void MinusIntellect()
        {
            playerState.CountIntellect(-1);
            playerState.Init();
            SetStatePanel();
        }

        public void Save()
        {
            sm.state = Instantiate(playerState);
            sm.state.Init();
            UIManager.Instance.ReturnPrev();
        }

        public void Restore()
        {
            playerState = Instantiate(sm.state);
            playerState.Init();
            InitStatePanel();
        }

        public override void Show()
        {
            playerState = Instantiate(sm.state);
            playerState.Init();
            InitStatePanel();
            stateView.parent.SetActive(true);
        }

        public override void Hide()
        {
            stateView.parent.SetActive(false);
        }
    }
    public Controller controller;

    public ActorManager am;
    private StateView stateView;
    private void Start()
    {
        stateView = GetComponent<StateView>();
        controller.Init(am, stateView);
    }
}
