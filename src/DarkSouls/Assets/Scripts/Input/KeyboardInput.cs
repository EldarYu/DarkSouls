using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class KeyboardInput : IPlayerInput
{
    [SerializeField]
    private Button jumpBtn = new Button();

    [Header("Key Settings")]
    public KeyCode forwardKey;
    public KeyCode backKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upArrowKey;
    public KeyCode downArrowKey;
    public KeyCode leftArrowKey;
    public KeyCode rightArrowKey;
    public KeyCode runKey;
    public KeyCode jumpKey;
    public KeyCode attackKey;
    public KeyCode defenseKey;

    void Update()
    {
        runBtn.Tick(Input.GetKey(runKey), Time.deltaTime);
        jumpBtn.Tick(Input.GetKey(jumpKey), Time.deltaTime);
        attackBtn.Tick(Input.GetKey(attackKey), Time.deltaTime);
        defenseBtn.Tick(Input.GetKey(defenseKey), Time.deltaTime);

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

        Vector2 axis = SquareToCircle(new Vector2(Dright, Dup));
        Dmag = Mathf.Sqrt(axis.y * axis.y + axis.x * axis.x);
        Dvec = axis.x * transform.right + axis.y * transform.forward;

        Run = runBtn.IsPressing && !runBtn.IsDelaying;
        Jump = jumpBtn.OnPressed && (runBtn.IsPressing || runBtn.IsExtending);
        Roll = jumpBtn.OnReleased && jumpBtn.IsDelaying;
        Attack = attackBtn.OnPressed;
        Defense = defenseBtn.IsPressing;
    }
}
