using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayerInput : MonoBehaviour
{
    public bool inputEnabled = true;
    public float Dmag { get; protected set; }
    public Vector3 Dvec { get; protected set; }
    public float Jup { get; protected set; }
    public float Jright { get; protected set; }
    public bool Run { get; protected set; }
    public bool Jump { get; protected set; }
    public bool Roll { get; protected set; }
    public bool LockOn { get; protected set; }
    public bool LeftAttack { get; protected set; }
    public bool RightAttack { get; protected set; }
    public bool LeftHeavyAttack { get; protected set; }
    public bool RightHeavyAttack { get; protected set; }
    public bool Defense { get; protected set; }
    public bool Action { get; protected set; }
    public bool ShortcutLeftSelect { get; protected set; }
    public bool ShortcutRightSelect { get; protected set; }
    public bool ShortcutTopSelect { get; protected set; }
    public bool ShortcutDownSelect { get; protected set; }
    public bool ShortcuLeftUse { get; protected set; }
    public bool ShortcuRightUse { get; protected set; }
    public bool ShortcuTopUse { get; protected set; }
    public bool ShortcuDownUse { get; protected set; }

    protected float Dup;
    protected float targetDup;
    protected float Dright;
    protected float targetDright;
    protected float velovityDup;
    protected float velovityDright;

    protected Vector2 SquareToCircle(Vector2 axis)
    {
        Vector2 output = Vector2.zero;
        output.x = axis.x * Mathf.Sqrt(1 - (axis.y * axis.y) / 2.0f);
        output.y = axis.y * Mathf.Sqrt(1 - (axis.x * axis.x) / 2.0f);

        return output;
    }

    protected void UpdateDmagDvec(float dup, float dright)
    {
        Vector2 axis = SquareToCircle(new Vector2(dright, dup));
        Dmag = Mathf.Sqrt(axis.y * axis.y + axis.x * axis.x);
        Dvec = axis.x * transform.right + axis.y * transform.forward;
    }
}
