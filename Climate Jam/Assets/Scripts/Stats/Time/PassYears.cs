using System.Collections;
using UnityEngine;
using System;

public class PassYears : MonoBehaviour {

    public float num_Years_Passed;
    [SerializeField]
    private float years_Per_Min;
    public DateTime date_Last_Opened;

   


	public IEnumerator PassYearsRealtime()
    {
        while(true)
        {
            num_Years_Passed += years_Per_Min /60;
            yield return new WaitForSeconds(1);
        }
    }
}
