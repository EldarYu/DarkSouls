using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [System.Serializable]
    public class Controller : IUIController
    {
        public StateManager stateManager;
        private StateView stateView;
        private State playerState;
        public void Init(StateView _stateView)
        {
            if (stateManager == null)
                return;
            stateView = _stateView;

            playerState = Instantiate(stateManager.state);
        }

        public void SetStatePanel()
        {
            stateView.view.level.text = playerState.Level.ToString();
            stateView.view.curHp.text = ((int)playerState.HP).ToString();
            stateView.view.maxHp.text = ((int)playerState.MaxHP).ToString();
            stateView.view.curVigor.text = ((int)playerState.Vigor).ToString();
            stateView.view.maxVigor.text = ((int)playerState.MaxVigor).ToString();
            stateView.view.curMp.text = ((int)playerState.MP).ToString();
            stateView.view.maxMp.text = ((int)playerState.MaxMP).ToString();
            stateView.view.strength.text = playerState.Strength.ToString();
            stateView.view.stamina.text = playerState.Stamina.ToString();
            stateView.view.Intellect.text = playerState.Intellect.ToString();
        }

        public void AddStrength()
        {
            playerState.Strength++;
            playerState.Init();
            SetStatePanel();
        }

        public void AddStamina()
        {
            playerState.Stamina++;
            playerState.Init();
            SetStatePanel();
        }

        public void AddIntellect()
        {
            playerState.Intellect++;
            playerState.Init();
            SetStatePanel();
        }

        public void Save()
        {
            stateManager.state = Instantiate(playerState);
            UIManager.Instance.ReturnPrev();
        }

        public void Restore()
        {
            playerState = Instantiate(stateManager.state);
            SetStatePanel();
        }

        public override void Show()
        {
            SetStatePanel();
            stateView.parent.SetActive(true);
        }

        public override void Hide()
        {
            stateView.parent.SetActive(false);
        }
    }
    public Controller controller;

    private StateView stateView;
    private void Start()
    {
        stateView = GetComponent<StateView>();
        controller.Init(stateView);
    }
}
