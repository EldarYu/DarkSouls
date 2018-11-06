using UnityEngine;

[System.Serializable]
public class Button
{
    public bool IsPressing { get; private set; }
    public bool OnPressed { get; private set; }
    public bool OnReleased { get; private set; }
    public bool IsExtending { get; private set; }
    public bool IsDelaying { get; private set; }

    public float extendingDuration;
    public float delayingDuration;
    private bool curState = false;
    private bool lastState = false;

    private Timer extTimer = new Timer();
    private Timer delayTimer = new Timer();

    public void Tick(bool input, float dt)
    {
        extTimer.Tick(dt);
        delayTimer.Tick(dt);
        curState = input;
        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;

        if (curState != lastState)
        {
            if (curState)
            {
                OnPressed = true;
                delayTimer.Go(delayingDuration);
            }
            else
            {
                OnReleased = true;
                extTimer.Go(extendingDuration);
            }
        }

        lastState = curState;

        IsExtending = extTimer.IsRunning();
        IsDelaying = delayTimer.IsRunning();
    }

}

