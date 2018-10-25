using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IPlayerInput
{
    public KeyCode moveForward;
    public KeyCode moveBack;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode run;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    void Update()
    {
        if (!inputEnabled)
            return;

        targetDup = (Input.GetKey(moveForward) ? 1.0f : 0) - (Input.GetKey(moveBack) ? 1.0f : 0);
        targetDright = (Input.GetKey(moveRight) ? 1.0f : 0) - (Input.GetKey(moveLeft) ? 1.0f : 0);

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velovityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velovityDright, 0.1f);
        SquareToCircle(ref Dup, ref Dright);

        Jup = (Input.GetKey(up) ? 1.0f : 0) - (Input.GetKey(down) ? 1.0f : 0);
        Jright = (Input.GetKey(right) ? 1.0f : 0) - (Input.GetKey(left) ? 1.0f : 0);

        Dmag = Mathf.Sqrt(Dup * Dup + Dright * Dright);
        Dvec = Dup * transform.forward + Dright * transform.right;

        Run = Input.GetKey(run);
    }
}
