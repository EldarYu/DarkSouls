using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IPlayerInput
{
    [Header("Key Settings")]
    public KeyMap keymap;

    private Button runBtn = new Button();
    private Button rightAtkBtn = new Button();
    private Button leftAtkBtn = new Button();
    private Button rightHAtkBtn = new Button();
    private Button leftHAtkBtn = new Button();
    private Button lockBtn = new Button();
    private Button actionBtn = new Button();
    private Button itemTopBtn = new Button();
    private Button itemDownBtn = new Button();
    private Button itemLeftBtn = new Button();
    private Button itemRightBtn = new Button();
    private Button itemUseBtn = new Button();

    private void Start()
    {
        keymap = Settings.Instance.keyMap;
    }

    void Update()
    {
        runBtn.Tick(Input.GetKey(keymap.runKey), Time.deltaTime);
        leftAtkBtn.Tick(Input.GetKey(keymap.leftAtkKey), Time.deltaTime);
        rightAtkBtn.Tick(Input.GetKey(keymap.rightAtkKey), Time.deltaTime);
        leftHAtkBtn.Tick(Input.GetKey(keymap.leftHAtkKey), Time.deltaTime);
        rightHAtkBtn.Tick(Input.GetKey(keymap.rightHAtkKey), Time.deltaTime);
        lockBtn.Tick(Input.GetKey(keymap.lockKey), Time.deltaTime);
        actionBtn.Tick(Input.GetKey(keymap.actionKey), Time.deltaTime);
        itemLeftBtn.Tick(Input.GetKey(keymap.itemLeftKey), Time.deltaTime);
        itemRightBtn.Tick(Input.GetKey(keymap.itemRightKey), Time.deltaTime);
        itemTopBtn.Tick(Input.GetKey(keymap.itemUpKey), Time.deltaTime);
        itemDownBtn.Tick(Input.GetKey(keymap.itemDownKey), Time.deltaTime);
        itemUseBtn.Tick(Input.GetKey(keymap.itemUseKey), Time.deltaTime);

        Jup = (Input.GetKey(keymap.upArrowKey) ? 1.0f : 0) - (Input.GetKey(keymap.downArrowKey) ? 1.0f : 0);
        Jright = (Input.GetKey(keymap.rightArrowKey) ? 1.0f : 0) - (Input.GetKey(keymap.leftArrowKey) ? 1.0f : 0);

        targetDup = (Input.GetKey(keymap.forwardKey) ? 1.0f : 0) - (Input.GetKey(keymap.backwardKey) ? 1.0f : 0);
        targetDright = (Input.GetKey(keymap.rightKey) ? 1.0f : 0) - (Input.GetKey(keymap.leftKey) ? 1.0f : 0);

        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velovityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velovityDright, 0.1f);

        UpdateDmagDvec(Dup, Dright);

        Run = (runBtn.IsPressing && !runBtn.IsDelaying) || runBtn.IsExtending;
        Jump = runBtn.OnPressed && Run;
        Roll = runBtn.OnReleased && runBtn.IsDelaying;
        LeftAttack = leftAtkBtn.OnReleased;
        RightAttack = rightAtkBtn.OnReleased;
        LeftHeavyAttack = leftHAtkBtn.OnReleased;
        RightHeavyAttack = rightHAtkBtn.OnReleased;
        Defense = leftAtkBtn.IsPressing;
        LockOn = lockBtn.OnPressed;
        Action = actionBtn.OnPressed;

        ShortcutLeftSelect = itemLeftBtn.OnReleased && itemLeftBtn.IsDelaying;
        ShortcutRightSelect = itemRightBtn.OnReleased && itemRightBtn.IsDelaying;
        ShortcutTopSelect = itemTopBtn.OnReleased && itemTopBtn.IsDelaying;
        ShortcutDownSelect = itemDownBtn.OnReleased && itemDownBtn.IsDelaying;

        ShortcutItemUse = itemUseBtn.OnReleased && itemUseBtn.IsDelaying;
    }
}
