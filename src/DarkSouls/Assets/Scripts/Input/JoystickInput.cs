using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class JoystickInput : IPlayerInput
{
    private void Update()
    {
        Run = runBtn.IsPressing && !runBtn.IsDelaying;
        Jump = runBtn.OnPressed && runBtn.IsExtending;
        Roll = runBtn.OnReleased && runBtn.IsDelaying;
    }
}
