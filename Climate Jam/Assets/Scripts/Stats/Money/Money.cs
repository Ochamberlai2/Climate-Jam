using System.Collections;
using UnityEngine;

public class Money : Stat {

    public float current_Money;

	public IEnumerator GainMoney()
    {
        //every second, increase money by the base value + the attached modifiers
        while(true)
        {
            current_Money += total_Value;
            GlobalBlackboard.Instance.statUI.UpdateMoneyUI(current_Money);
            yield return new WaitForSeconds(1);
        }
    }
}
