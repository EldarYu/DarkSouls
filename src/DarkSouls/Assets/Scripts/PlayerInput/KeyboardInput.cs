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
    private Button runBtn = new Button();
    [SerializeField]
    private Button rightAtkBtn = new Button();
    [SerializeField]
    private Button leftAtkBtn = new Button();
    [SerializeField]
    private Button defenseBtn = new Button();
    [SerializeField]
    private Button lockBtn = new Button();

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
    public KeyCode defenseKey;
    public KeyCode lockKey;

    void Update()
    {
        runBtn.Tick(Input.GetKey(runKey), Time.deltaTime);
        jumpBtn.Tick(Input.GetKey(jumpKey), Time.deltaTime);
        leftAtkBtn.Tick(Input.GetKey(leftAttackKey), Time.deltaTime);
        rightAtkBtn.Tick(Input.GetKey(rightAttackKey), Time.deltaTime);
        lockBtn.Tick(Input.GetKey(lockKey), Time.deltaTime);
        defenseBtn.Tick(Input.GetKey(defenseKey), Time.deltaTime);

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
        LeftAttack = leftAtkBtn.OnReleased && leftAtkBtn.IsDelaying;
        RightAttack = rightAtkBtn.OnReleased && rightAtkBtn.IsDelaying;
        LeftHeavyAttack = leftAtkBtn.IsPressing && !leftAtkBtn.IsDelaying;
        RightHeavyAttack = rightAtkBtn.IsPressing && !rightAtkBtn.IsDelaying;
        Defense = defenseBtn.IsPressing;
        LockOn = lockBtn.OnPressed;
    }
}
