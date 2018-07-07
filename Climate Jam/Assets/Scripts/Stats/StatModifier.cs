using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModifierType
{
    Additive,
    Multiplicative,
    PercentAdditive,
}

[System.Serializable]
public class StatModifier
{


    [Header("For now, full num for add, 0-1 for the others")]
    public StatModifierType stat_Modifier_Type;
    public float value;
}
