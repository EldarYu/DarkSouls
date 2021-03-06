﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CheckState : Conditional
{
    [Header("HP % limit")]
    public float limit = 0.5f;
    private float hpLimit;
    private ArtoriasManager ArtorM;
    public override void OnStart()
    {
        ArtorM = GetComponent<ArtoriasManager>();
        hpLimit = ArtorM.maxBossHp * limit;
    }

    public override TaskStatus OnUpdate()
    {
        if (ArtorM.CurHp < hpLimit)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }

}

