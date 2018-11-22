using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IPlayerInput
{
    [Header("Button Settings")]
    [SerializeField]
    private Button runBtn = new Button();
    [SerializeField]
    private Button rightAtkBtn = new Button();
    [SerializeField]
    private Button leftAtkBtn = new Button();
    [SerializeField]
    private Button rightHAtkBtn = new Button();
    [SerializeField]
    private Button leftHAtkBtn = new Button();
    [SerializeField]
    private Button lockBtn = new Button();
    [SerializeField]
    private Button actionBtn = new Button();

    [Header("Key Settings")]
    public KeyCode forwardKey;
    public KeyCode backKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode runKey;
    public KeyCode upArrowKey;
    public KeyCode downArrowKey;
    public KeyCode leftArrowKey;
    public KeyCode rightArrowKey;
    public KeyCode rightAtkKey;
    public KeyCode leftAtkKey;
    public KeyCode rightHAtkKey;
    public KeyCode leftHAtkKey;
    public KeyCode lockKey;
    public KeyCode actionKey;

    void Update()
    {
        runBtn.Tick(Input.GetKey(runKey), Time.deltaTime);
        leftAtkBtn.Tick(Input.GetKey(leftAtkKey), Time.deltaTime);
        rightAtkBtn.Tick(Input.GetKey(rightAtkKey), Time.deltaTime);
        leftHAtkBtn.Tick(Input.GetKey(leftHAtkKey), Time.deltaTime);
        rightHAtkBtn.Tick(Input.GetKey(rightHAtkKey), Time.deltaTime);
        lockBtn.Tick(Input.GetKey(lockKey), Time.deltaTime);
        actionBtn.Tick(Input.GetKey(actionKey), Time.deltaTime);

        Jup = (Input.GetKey(upArrowKey) ? 1.0f : 0) - (Input.GetKey(downArrowKey) ? 1.0f : 0);
        Jright = (Input.GetKey(rightArrowKey) ? 1.0f : 0) - (Input.GetKey(leftArrowKey) ? 1.0f : 0);

        targetDup = (Input.GetKey(forwardKey) ? 1.0f : 0) - (Input.GetKey(backKey) ? 1.0f : 0);
        targetDright = (Input.GetKey(rightKey) ? 1.0f : 0) - (Input.GetKey(leftKey) ? 1.0f : 0);

        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velovityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velovityDright, 0.1f);

        UpdateDmagDvec(Dup, Dright);

        Run = runBtn.IsPressing && !runBtn.IsDelaying;
        Jump = runBtn.OnPressed && runBtn.IsExtending;
        Roll = runBtn.OnReleased && runBtn.IsDelaying;
        LeftAttack = leftAtkBtn.OnReleased;
        RightAttack = rightAtkBtn.OnReleased;
        LeftHeavyAttack = leftHAtkBtn.OnReleased;
        RightHeavyAttack = rightHAtkBtn.OnReleased;
        Defense = leftAtkBtn.IsPressing;
        LockOn = lockBtn.OnPressed;
        Action = actionBtn.OnPressed;
    }
}
