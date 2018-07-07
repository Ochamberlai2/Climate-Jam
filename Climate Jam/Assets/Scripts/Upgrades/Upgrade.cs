using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Upgrade {

    public int modifier_GHG_Effect;
    public StatModifier[]  upgrade_Stat_Modifiers;
    public string modifier_Name;
    [TextArea]
    public string modifier_Description;
    public Sprite modifier_Sprite;
    public int upgrade_Level;
    public int upgrade_Max_Level;

}
