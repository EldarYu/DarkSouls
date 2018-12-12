using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutController : MonoBehaviour
{
    [System.Serializable]
    public class Controller
    {
        private IPlayerInput pi;
        private ShortcutSlotView left;
        private ShortcutSlotView right;
        private ShortcutSlotView top;
        private ShortcutSlotView down;
        public void Init(ActorManager am, ShortcutView shortcutView)
        {
            left = shortcutView.left;
            right = shortcutView.right;
            top = shortcutView.top;
            down = shortcutView.down;
            pi = am.GetComponent<IPlayerInput>();
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

            if (pi.ShortcuLeftUse)
                ShortcutUse(left);

            if (pi.ShortcuRightUse)
                ShortcutUse(right);

            if (pi.ShortcuTopUse)
                ShortcutUse(top);

            if (pi.ShortcuDownUse)
                ShortcutUse(down);
        }

        public void ShortcutSelect(ShortcutSlotView tgt)
        {

        }

        public void ShortcutUse(ShortcutSlotView tgt)
        {

        }
    }
    public Controller controller;

    public ActorManager am;
    private ShortcutView shortcutView;
    private void Start()
    {
        shortcutView = GetComponent<ShortcutView>();
        controller.Init(am, shortcutView);
    }
}
