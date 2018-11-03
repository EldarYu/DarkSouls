using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private BattleManager bm;
    private ActorController ac;
    private void Awake()
    {
        bm = GetComponentInChildren<BattleManager>();
        ac = GetComponent<ActorController>();
    }

    public void DoDamage()
    {
        ac.SetHitTirgger();
    }
}
