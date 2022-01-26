using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChanger : MonoBehaviour
{

    public void TimeUpOneHour()
    {
        //shown time
        GlobalVars.timeHours += 1;
        GlobalVars.timeMinutes = 00;

        //time for 24 hours and decimals
        GlobalVars.timeHourshly += 1;
        GlobalVars.timeHourshly = UnityEngine.Mathf.Floor (GlobalVars.timeHourshly);
    }
    public void TimeDownOneHour()
    {
        //shown time
        GlobalVars.timeHours -= 1;
        GlobalVars.timeMinutes = 00;

        //time for 24 hours and decimals
        GlobalVars.timeHourshly -= 1;
        GlobalVars.timeHourshly = UnityEngine.Mathf.Floor (GlobalVars.timeHourshly);
    }

}
