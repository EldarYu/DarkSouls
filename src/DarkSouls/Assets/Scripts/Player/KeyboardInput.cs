using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IPlayerInput
{
    public KeyCode forward;
    public KeyCode back;
    public KeyCode left;
    public KeyCode right;
    public KeyCode run;
    public KeyCode upArrow;
    public KeyCode downArrow;
    public KeyCode leftArrow;
    public KeyCode rightArrow;
    public KeyCode jump;

    private bool lastJump;

    void Update()
    {


        Jup = (Input.GetKey(upArrow) ? 1.0f : 0) - (Input.GetKey(downArrow) ? 1.0f : 0);
        Jright = (Input.GetKey(rightArrow) ? 1.0f : 0) - (Input.GetKey(leftArrow) ? 1.0f : 0);

        targetDup = (Input.GetKey(forward) ? 1.0f : 0) - (Input.GetKey(back) ? 1.0f : 0);
        targetDright = (Input.GetKey(right) ? 1.0f : 0) - (Input.GetKey(left) ? 1.0f : 0);

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

        Run = Input.GetKey(run);

        bool temp = Input.GetKeyDown(jump);

        if (temp != lastJump && temp)
            Jump = true;
        else
            Jump = false;

        lastJump = temp;
    }
}
