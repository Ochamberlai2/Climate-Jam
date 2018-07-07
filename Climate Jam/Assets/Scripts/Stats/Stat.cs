using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour {

    
    protected List<StatModifier> stat_Modifiers;
    [Space]
    [SerializeField]
    protected float base_Value;
    protected bool is_Dirty = true;
    protected float last_Value;

    public float total_Value
    {
        get
        {
            if(is_Dirty)
            {
                last_Value = CalculateFinalValue();
                is_Dirty = false;
            }
            return last_Value;
        }
    }

    /// <summary>
    /// Calculate the total value of the modifiers
    /// </summary>
    /// <returns>the final value of the base value and the modifiers</returns>
    virtual protected float CalculateFinalValue()
    {
        float final_value = base_Value;
        float percent_additive_value = 0;

        if (stat_Modifiers == null || stat_Modifiers.Count == 0)
            return final_value;

        //Loop through all attached stat modifiers
        for(int i = 0; i < stat_Modifiers.Count; i++)
        {
            //depending on the modifier type, different maths needs to happen
            switch(stat_Modifiers[i].stat_Modifier_Type)
            {
                //if it's additive, add the flat value to the final value 
                case StatModifierType.Additive:
                    final_value += stat_Modifiers[i].value;
                    break;
                 //if it's multiplicative, multiply it by the value
                case StatModifierType.Multiplicative:
                    final_value *= 1 + stat_Modifiers[i].value;
                    break;
                 //if it's percent add, we need to add the value to a temporary variable, then once the modifier at the next index is not
                 //percent additive, or is the end of the list, reset the variable.
                case StatModifierType.PercentAdditive:
                    percent_additive_value += stat_Modifiers[i].value;
                    if(stat_Modifiers[i + 1].stat_Modifier_Type != StatModifierType.PercentAdditive || i +1 >= stat_Modifiers.Count)
                    {
                        final_value *= 1 + percent_additive_value;
                        percent_additive_value = 0;
                    }
                    break;
            }
        }
        return Mathf.Round(final_value);
    }
    /// <summary>
    ///add a modifier to the list of modifiers
    /// </summary>
    /// <param name="mod">modififer to add</param>
    virtual public void AddModifier(StatModifier mod)
    {
        stat_Modifiers.Add(mod);
        is_Dirty = true;
    }
    /// <summary>
    /// remove a modifier from the list of modifiers
    /// </summary>
    /// <param name="mod">modifier to remove</param>
    /// <returns>whether or not the modifier was removed</returns>
    virtual public bool RemoveModifier(StatModifier mod)
    {
        if(stat_Modifiers.Contains(mod))
        {
            stat_Modifiers.Remove(mod);
            is_Dirty = true;
            return true;
        }
        return false;

    }
}
