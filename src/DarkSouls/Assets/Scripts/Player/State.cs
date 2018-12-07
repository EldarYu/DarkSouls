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
            strength = Mathf.Clamp(value, strength, 99);
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
            stamina = Mathf.Clamp(value, stamina, 99);
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
            intellect = Mathf.Clamp(value, intellect, 99);
        }
    }

    public float hpIncrement;
    public float vigorIncrement;
    public float mpIncrement;
    public float attackIncrement;
    public float levelIncrement;

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
}

