using UnityEngine;

public class RegionSelectDebugUI : MonoBehaviour {

    private UpgradeUI upgradeUI;

    public void Start()
    {
        upgradeUI = FindObjectOfType<UpgradeUI>();
    }

    public void SelectRegion()
    {
        upgradeUI.SetSelectedRegion((Region)transform.GetSiblingIndex());
    }
}
