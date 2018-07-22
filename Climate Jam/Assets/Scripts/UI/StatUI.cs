using UnityEngine.UI;
using UnityEngine;


public class StatUI : MonoBehaviour {

    [SerializeField]
    private Text money;
    [SerializeField]
    private Text years_played;
    [SerializeField]
    private Text greenhouse_gases;

    public void UpdateMoneyUI(float moneyAmt)
    {
        money.text = "£" + Mathf.RoundToInt(moneyAmt).ToString();
    }
    public void UpdateYearsUI(float yearsAmt)
    {
        years_played.text = Mathf.RoundToInt(yearsAmt).ToString() + " Years Played";
    }
    public void UpdateGHGUI(Region region)
    {
        greenhouse_gases.text = "Greenhouse Gas Level: " + GlobalBlackboard.Instance.regions[(int)region].GHG_Level.ToString();
    }
}
