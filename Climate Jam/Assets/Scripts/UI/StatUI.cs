using UnityEngine.UI;
using UnityEngine;


public class StatUI : MonoBehaviour {

    [SerializeField]
    private Text money;
    [SerializeField]
    private Text years_played;
    [SerializeField]
    private Text greenhouse_gases;

    //THIS NEEDS TO BE CHANGED BEFORE UI IS FINALISED
    void Update ()
    {
        money.text = "£" + GlobalBlackboard.Instance.money.current_Money.ToString();
        years_played.text = Mathf.RoundToInt(GlobalBlackboard.Instance.time.num_Years_Passed).ToString() + " Years Played";

	}
}
