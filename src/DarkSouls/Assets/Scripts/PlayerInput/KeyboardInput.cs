using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class KeyboardInput : IPlayerInput
{
    [Header("Button Settings")]
    [SerializeField]
    private Button jumpBtn = new Button();
    [SerializeField]
    protected Button runBtn = new Button();
    [SerializeField]
    protected Button rightAttackBtn = new Button();
    [SerializeField]
    protected Button leftAttackBtn = new Button();
    [SerializeField]
    protected Button lockBtn = new Button();

    [Header("Mouse Settings")]
    [Range(0, 1)]
    public float mouseSensitivityX = 0.3f;
    [Range(0, 1)]
    public float mouseSensitivityY = 0.3f;

    [Header("Key Settings")]
    public KeyCode forwardKey;
    public KeyCode backKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode runKey;
    public KeyCode jumpKey;
    public KeyCode rightAttackKey;
    public KeyCode leftAttackKey;
    public KeyCode lockKey;

    void Update()
    {
        runBtn.Tick(Input.GetKey(runKey), Time.deltaTime);
        jumpBtn.Tick(Input.GetKey(jumpKey), Time.deltaTime);
        leftAttackBtn.Tick(Input.GetKey(leftAttackKey), Time.deltaTime);
        rightAttackBtn.Tick(Input.GetKey(rightAttackKey), Time.deltaTime);
        lockBtn.Tick(Input.GetKey(lockKey), Time.deltaTime);

        Jup = Input.GetAxis("Mouse Y") * 10.0f * mouseSensitivityY;
        Jright = Input.GetAxis("Mouse X") * 10.0f * mouseSensitivityX;

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

        Run = runBtn.IsPressing;
        Jump = jumpBtn.OnPressed && (runBtn.IsPressing || runBtn.IsExtending);
        Roll = jumpBtn.OnReleased && jumpBtn.IsDelaying;
        LeftAttack = leftAttackBtn.OnPressed;
        RightAttack = rightAttackBtn.OnPressed;
        Defense = leftAttackBtn.IsPressing;
        LockOn = lockBtn.OnPressed;
    }
}
