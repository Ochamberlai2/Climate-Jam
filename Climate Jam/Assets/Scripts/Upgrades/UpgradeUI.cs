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

    [SerializeField]
    private Sprite upgrade_Bought_Image;
    [SerializeField]
    private Sprite upgrade_Not_Bought_Image;

    [SerializeField]
    private GameObject scrollview;

    public void Start()
    {
        buttons = new GameObject[button_Pool_Size];
        for (int i = 0; i < button_Pool_Size; i++)
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
        int buttonIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
        //get the correct upgrade using the button index
        Upgrade upgrade = GlobalBlackboard.Instance.regions[(int)currently_Selected_Region].region_Upgrades[buttonIndex];
        //if the upgrade's level is less than max and the player has enough money to buy it, upgrade
        if (upgrade.upgrade_Level < upgrade.upgrade_Max_Level && GlobalBlackboard.Instance.money.current_Money >= upgrade.upgrade_Cost[upgrade.upgrade_Level])
        {
            //add a modifier to the money object
            GlobalBlackboard.Instance.money.AddModifier(upgrade.upgrade_Stat_Modifiers[upgrade.upgrade_Level]);
            //increase GHG level
            GlobalBlackboard.Instance.regions[(int)currently_Selected_Region].GHG_Level += upgrade.modifier_GHG_Effect;
            //reduce the current amount of money by the upgrade's cost
            GlobalBlackboard.Instance.money.current_Money -= upgrade.upgrade_Cost[upgrade.upgrade_Level];
            //update the money ui
            GlobalBlackboard.Instance.statUI.UpdateMoneyUI(GlobalBlackboard.Instance.money.current_Money);
            //upgrade the greenhouse gas levels
            GlobalBlackboard.Instance.statUI.UpdateGHGUI(currently_Selected_Region);
            
            //update the upgrade bought button
            buttons[buttonIndex].transform.Find("Level Pips").GetChild(upgrade.upgrade_Level).GetChild(0).GetComponent<Image>().sprite = upgrade_Bought_Image;

            upgrade.upgrade_Level++;
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
            buttons[i].transform.Find("Name").GetComponent<Text>().text = worldRegion.region_Upgrades[i].upgrade_Name;
            buttons[i].transform.Find("Button").GetChild(0).GetComponent<Image>().sprite = worldRegion.region_Upgrades[i].upgrade_Sprite;
           for (int j = 0; j < worldRegion.region_Upgrades[i].upgrade_Max_Level; j++ )
            {
                //if the upgrade has been bought, set it's pip's sprite to the bought image
                if ( j < worldRegion.region_Upgrades[i].upgrade_Level )
                {
                    buttons [i].transform.Find("Level Pips").GetChild(j).GetChild(0).GetComponent<Image>().sprite = upgrade_Bought_Image;
                }
                buttons[i].transform.Find("Level Pips").GetChild(j).gameObject.SetActive(true);
            }

        }
    }
    public void ResetButtons()
    {
        for(int i = 0; i < button_Pool_Size; i++)
        {
            //loop through all of the level pips, set the image to not bought and deactivate them
            for(int j = 0; j < buttons[i].transform.Find("Level Pips").childCount; j++)
            {
                buttons[i].transform.Find("Level Pips").GetChild(j).GetChild(0).GetComponent<Image>().sprite = upgrade_Not_Bought_Image;
                buttons[i].transform.Find("Level Pips").GetChild(j).gameObject.SetActive(false);
            }
            //deactivate the object
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

        //if the region isnt worldwide, nullify the region's camera's porthole
        if(currently_Selected_Region != Region.Worldwide)
            GlobalBlackboard.Instance.regions[(int)currently_Selected_Region].region_Camera.targetTexture = null;


        currently_Selected_Region = region;

        //if the region isnt worldwide, set the render texure of the region's camera.
        if (region != Region.Worldwide)
        {
            GlobalBlackboard.Instance.regions[(int)region].region_Camera.targetTexture = renderTexture;
            //activate the scrollview if it's inactive
            if(!scrollview.activeSelf)
            {
                scrollview.SetActive(true);
            }
        }
        else
        {
            if(scrollview.activeSelf)
            {
                scrollview.SetActive(false);
            }
        }
        SelectRegion(region);
    }
}
