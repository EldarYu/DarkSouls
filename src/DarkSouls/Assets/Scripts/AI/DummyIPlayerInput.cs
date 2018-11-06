using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIPlayerInput : IPlayerInput
{
    IEnumerator Start()
    {
        while (true)
        {
            RightAttack = true;
            yield return new WaitForSeconds(1);
        }
    }

    private void Update()
    {
        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }

        UpdateDmagDvec(Dup, Dright);
    }
}
