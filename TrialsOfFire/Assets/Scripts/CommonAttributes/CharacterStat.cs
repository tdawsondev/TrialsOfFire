using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    public float baseValue = 10f;
    public List<StatModifier> modifiers;

    public CharacterStat()
    {
        modifiers = new List<StatModifier>();
    }
    public CharacterStat(float baseValue): this()
    {
        this.baseValue = baseValue;
    }
    private bool isDirty = true;
    private float lastValue = 10f;

    public float Value
    {
        get
        {
            float value = lastValue;
            if (isDirty)
            {
                value = CalculateValue();
                lastValue = value;
                isDirty = false;
            }
            return value;
        }
    }



    public float CalculateValue()
    {
        float finalValue = baseValue;
        foreach(StatModifier modifier in modifiers)
        {
            finalValue *= 1+ modifier.Value;
        }


        return (float)Math.Round(finalValue, 3);
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        modifiers.Add(mod);
    }
    public bool RemoveModifier(StatModifier mod)
    {
        if (modifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }
}
