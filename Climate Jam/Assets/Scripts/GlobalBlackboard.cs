using System;
using UnityEngine;

public class GlobalBlackboard : MonoBehaviour
{

    #region singleton
    static GlobalBlackboard global_Blackboard;

    public static GlobalBlackboard Instance
    {
        get
        {
            if(global_Blackboard == null)
            {
                global_Blackboard = FindObjectOfType<GlobalBlackboard>();
                if (global_Blackboard == null)
                {
                    GameObject newGameObject = new GameObject();
                    newGameObject.name = "Global Blackboard";
                    global_Blackboard = newGameObject.AddComponent<GlobalBlackboard>();
                }
            }
            return global_Blackboard;
        }
    }
#endregion

    public Money money;
    public PassYears time;
    public GlobalInformation GHG;


    public void Start()
    {
        //give money for amount of time passed
        TimeSpan timePassed = DateTime.UtcNow - time.date_Last_Opened;
        money.current_Money += money.total_Value * timePassed.Minutes;
        //start the passing of time
        time.StartCoroutine(time.PassYearsRealtime());
        //start gaining money
        money.StartCoroutine(money.GainMoney());
    }

    private void OnApplicationQuit()
    {
        time.date_Last_Opened = DateTime.UtcNow;
        //save the time last opened
        time.StopCoroutine(time.PassYearsRealtime());

        //stop gaining money
        money.StopCoroutine(money.GainMoney());

    }


}


