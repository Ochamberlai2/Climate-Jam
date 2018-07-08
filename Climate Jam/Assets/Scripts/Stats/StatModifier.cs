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

    public StatModifierType stat_Modifier_Type;
    public float value;
}
