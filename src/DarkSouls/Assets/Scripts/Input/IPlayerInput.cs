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
    public bool Attack { get; protected set; }
    public bool Roll { get; protected set; }
    public bool Defense { get; protected set; }
    public bool LockOn { get; protected set; }

    [Header("Button Settings")]
    [SerializeField]
    protected Button runBtn = new Button();
    [SerializeField]
    protected Button attackBtn = new Button();
    [SerializeField]
    protected Button defenseBtn = new Button();
    [SerializeField]
    protected Button lockBtn = new Button();

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
}
