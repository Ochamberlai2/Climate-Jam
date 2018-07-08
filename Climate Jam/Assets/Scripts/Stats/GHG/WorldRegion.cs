using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Region
{
    Worldwide,
    City,
    Farmland,
    Forest,
    Ocean,
}
[System.Serializable]
public class WorldRegion {

    public int GHG_Level;
    [SerializeField]
    private Region region;
    public List<Upgrade> region_Upgrades;
    //insert global effect object/list here
	
    public Region GetRegion()
    {
        return region;
    }

}
