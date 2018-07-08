using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Upgrade {

    public int modifier_GHG_Effect;
    public StatModifier[]  upgrade_Stat_Modifiers;
    public string upgrade_Name;
    [TextArea]
    public string upgrade_Description;
    public Sprite upgrade_Sprite;
    public int upgrade_Max_Level;
    public int upgrade_Level =1;
    public int[] upgrade_Cost;

}
