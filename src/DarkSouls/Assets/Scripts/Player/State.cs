using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
[CreateAssetMenu(menuName = "Player/Player State")]
public class State : ScriptableObject
{
    public int Level { get; private set; }
    private float hp;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = Mathf.Clamp(value, 0, MaxHP);
        }
    }
    public float MaxHP { get; private set; }
    private float vigor;
    public float Vigor
    {
        get
        {
            return vigor;
        }
        set
        {
            vigor = Mathf.Clamp(value, 0, MaxVigor);
        }
    }
    public float MaxVigor { get; private set; }
    private float mp;
    public float MP
    {
        get
        {
            return mp;
        }
        set
        {
            mp = Mathf.Clamp(value, 0, MaxMP);
        }
    }
    public float MaxMP { get; private set; }
    public float Attack { get; private set; }

    [Range(0, 99)]
    public int strength;
    public int Strength
    {
        get
        {
            return strength;
        }
        set
        {
            strength = Mathf.Clamp(value, 0, 99);
        }
    }
    [Range(0, 99)]
    public int stamina;
    public int Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = Mathf.Clamp(value, 0, 99);
        }
    }
    [Range(0, 99)]
    public int intellect;
    public int Intellect
    {
        get
        {
            return intellect;
        }
        set
        {
            intellect = Mathf.Clamp(value, 0, 99);
        }
    }

    public long souls;

    public float hpIncrement;
    public float vigorIncrement;
    public float mpIncrement;
    public float attackIncrement;

    public long upgradeSoulAmount;

    public long RequiredForUpgrade { get { return Level * upgradeSoulAmount; } }
    public long LastUpgradeSouls { get { return (Level - 1) * upgradeSoulAmount; } }

    public float runCost = 1.0f;
    public float rollCost = 15.0f;
    public float attackCost = 5.0f;
    public float heavyAttackCost = 15.0f;

    public float vigorAutoRecoverTime = 1.0f;
    public float vigorAutoRecoverAmount = 0.3f;

    public void Init()
    {
        Calculate();
        hp = MaxHP;
        vigor = MaxVigor;
        mp = MaxMP;
    }

    public void Calculate()
    {
        MaxHP = strength * hpIncrement;
        MaxVigor = stamina * vigorIncrement;
        MaxMP = intellect * mpIncrement;
        Attack = strength * attackIncrement;
        Level = (strength + stamina + intellect) - 45 + 1;
    }

    public bool CanUpgrade(long curSoulAmount)
    {
        return curSoulAmount >= RequiredForUpgrade;
    }

    public void CountStrength(int amount)
    {
        if (amount > 0)
        {
            if (!CanUpgrade(souls))
                return;
            souls -= RequiredForUpgrade;
        }
        else if (amount < 0)
            souls += LastUpgradeSouls;
        Strength += amount;
    }

    public void CountStamina(int amount)
    {
        if (amount > 0)
        {
            if (!CanUpgrade(souls))
                return;
            souls -= RequiredForUpgrade;
        }
        else if (amount < 0)
            souls += LastUpgradeSouls;
        Stamina += amount;
    }
    public void CountIntellect(int amount)
    {
        if (amount > 0)
        {
            if (!CanUpgrade(souls))
                return;
            souls -= RequiredForUpgrade;
        }
        else if (amount < 0)
            souls += LastUpgradeSouls;
        Intellect += amount;
    }

}

