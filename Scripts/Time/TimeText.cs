using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public TimeSun sunScript;
    public string time;
    private Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVars.timeMinutes < 10)
        {
            time = GlobalVars.timeHours + ":" + "0" + GlobalVars.timeMinutes + GlobalVars.amPm;
        }
        else
        {
            time = GlobalVars.timeHours + ":" + GlobalVars.timeMinutes + GlobalVars.amPm;
        }
        timeText.text = time;
    }
}
