using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionSwitch : MonoBehaviour {

    UpgradeUI upgrade_UI;

    public void Start()
    {
        upgrade_UI = FindObjectOfType<UpgradeUI>();
    }


    public void OnMouseDown()
    {
        upgrade_UI.SetSelectedRegion(Region.Worldwide);
    }

    public void SetSelectedRegionFarm()
    {
        upgrade_UI.SetSelectedRegion(Region.Farmland);
    }
    public void SetSelectedRegionOcean()
    {
        upgrade_UI.SetSelectedRegion(Region.Ocean);
    }
    public void SetSelectedRegionCity()
    {
        upgrade_UI.SetSelectedRegion(Region.City);
    }
    public void SetSelectedRegionForest()
    {
        upgrade_UI.SetSelectedRegion(Region.Forest);
    }
}
