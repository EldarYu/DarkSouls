using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IActorManager : MonoBehaviour
{
    public virtual Animator GetAnimator() { return null; }
    public virtual void LockUnlockAnimator(bool value = true) { }
}
