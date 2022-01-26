using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSun : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVars.timeHourshly > 5 && GlobalVars.timeHourshly <= 7) //2.6
        {
            
        }
        else if (GlobalVars.timeHourshly > 7 && GlobalVars.timeHourshly <= 17) //5.6
        {
            
        }
        else if (GlobalVars.timeHourshly > 17 && GlobalVars.timeHourshly <= 18.5) //2.6
        {
            
        }
        else
        {
            
        }

        transform.rotation = Quaternion.Euler(0, (GlobalVars.timeHourshly + GlobalVars.addTime) * 15 * GlobalVars.multiplierForTime, 0);
    }
}
