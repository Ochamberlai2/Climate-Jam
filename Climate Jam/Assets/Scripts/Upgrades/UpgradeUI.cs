using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUI : MonoBehaviour {

    private Region currently_Selected_Region = Region.City;
    [SerializeField]
    private GameObject upgrade_Button_Prefab;
    private GameObject[] buttons;
    [SerializeField]
    private Transform upgrade_Panel;
    [SerializeField]
    private int button_Pool_Size = 10;
    [SerializeField]
    private RenderTexture renderTexture;


    public void Start()
    {
        buttons = new GameObject[button_Pool_Size];
        for(int i = 0; i < button_Pool_Size; i++)
        {
            buttons[i] = Instantiate(upgrade_Button_Prefab);
            buttons[i].transform.SetParent(upgrade_Panel);
            buttons[i].GetComponentInChildren<Button>().onClick.AddListener(PurchaseUpgrade);
            buttons[i].SetActive(false);
        }

    }


    public void PurchaseUpgrade()
    {
        //find the buttons sibling index
        int buttonIndex = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        //get the correct upgrade using the button index
        Upgrade upgrade = GlobalBlackboard.Instance.regions[(int)currently_Selected_Region].region_Upgrades[buttonIndex];
        //if the upgrade's level is less than max and the player has enough money to buy it, upgrade
        if (upgrade.upgrade_Level < upgrade.upgrade_Max_Level && GlobalBlackboard.Instance.money.current_Money >= upgrade.upgrade_Cost[upgrade.upgrade_Level])
        {
            //add a modifier to the money object
            GlobalBlackboard.Instance.money.AddModifier(upgrade.upgrade_Stat_Modifiers[upgrade.upgrade_Level]);
            GlobalBlackboard.Instance.regions[(int)currently_Selected_Region].GHG_Level += upgrade.modifier_GHG_Effect;
            GlobalBlackboard.Instance.money.current_Money -= upgrade.upgrade_Cost[upgrade.upgrade_Level];
            upgrade.upgrade_Level++;
            buttons[buttonIndex].transform.Find("Text").GetComponent<Text>().text = upgrade.upgrade_Level.ToString();
            Debug.Log("upgrade purchased: " + upgrade.upgrade_Name);
        }

    }
    private void SelectRegion(Region region)
    {
        ResetButtons();
        //get the region of the world that is being changed to
        WorldRegion worldRegion = GlobalBlackboard.Instance.regions[(int)region];

       
        //loop through all of the upgrades in the region and set the buttons up
        for(int i = 0; i < worldRegion.region_Upgrades.Count; i++)
        {
            //make the button visible and interactable
            buttons[i].SetActive(true);
            //set the buttons text to show the upgrades name
            buttons[i].transform.Find("Button").Find("Text").GetComponent<Text>().text = worldRegion.region_Upgrades[i].upgrade_Name;
            //set the upgrade level text to the upgrade level
            buttons[i].transform.Find("Text").GetComponent<Text>().text = worldRegion.region_Upgrades[i].upgrade_Level.ToString();

        }
    }
    public void ResetButtons()
    {
        for(int i = 0; i < button_Pool_Size; i++)
        {
            buttons[i].SetActive(false);
        }
    }
    private void SetRenderTexture(Region region)
    {

            
            //set the render texture of the region's camera
           
           

    }
    //setter for selected region
    public void SetSelectedRegion(Region region)
    {

        if(currently_Selected_Region != Region.Worldwide)
            GlobalBlackboard.Instance.regions[(int)currently_Selected_Region].region_Camera.targetTexture = null;


        currently_Selected_Region = region;

        if (region != Region.Worldwide)
        {
            GlobalBlackboard.Instance.regions[(int)region].region_Camera.targetTexture = renderTexture;
        }
        SelectRegion(region);
    }
}
