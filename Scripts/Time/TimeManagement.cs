using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TimeManagement : MonoBehaviour
{
    private Scene House;
    private Scene Outside;
    private Scene currentScene;
    public Tilemap windows;
    public TileBase dayWindow;
    public TileBase nightWindow;

    //time stuff
    public float timeHours = 5;
    public float timeMinutes = 0;
    public string amPm = "am";
    public float timeHourshly = 5;
    public float multiplierForTime = 0.8f;
    public float addTime = 19;
    public bool extraAmPmTest = true;
    public Vector2 newPlayerPos = new Vector2(0, 0);
    public bool DayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(secondTimer());
        House = SceneManager.GetSceneByBuildIndex(1);
        Outside = SceneManager.GetSceneByBuildIndex(0);
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVars.timeMinutes >= 60)
        {
            //shown time
            GlobalVars.timeHours += 1;
            GlobalVars.timeMinutes = 00;

            //time for 24 hours and decimals
            GlobalVars.timeHourshly = UnityEngine.Mathf.Floor(GlobalVars.timeHourshly);
        }

        if (GlobalVars.timeHours == 12 && GlobalVars.amPm == "pm" && GlobalVars.extraAmPmTest == true)
        {
            GlobalVars.extraAmPmTest = false;
            GlobalVars.amPm = "am";
        }
        else if (GlobalVars.timeHours == 12 && GlobalVars.amPm == "am" && GlobalVars.extraAmPmTest == true)
        {
            GlobalVars.extraAmPmTest = false;
            GlobalVars.amPm = "pm";
        }

        if (GlobalVars.timeHours == 13)
        {
            GlobalVars.extraAmPmTest = true;
            GlobalVars.timeHours = 1;
        }

        if (GlobalVars.timeHourshly >= 24)
        {
            GlobalVars.timeHourshly = 0f;
        }

        if (GlobalVars.timeHourshly <= -1)
        {
            GlobalVars.timeHourshly = 23f;
        }

        if (GlobalVars.timeHours <= 0)
        {
            GlobalVars.timeHours = 12f;
        }

        if (GlobalVars.timeHourshly >= 17.99)
        {
            GlobalVars.DayTime = false;
        }
        else
        {
            GlobalVars.DayTime = true;
        }

        if (GlobalVars.DayTime == true && currentScene == House)
        {
            windows.SwapTile(nightWindow, dayWindow);
        }
        else if (GlobalVars.DayTime == false && currentScene == House)
        {
            windows.SwapTile(dayWindow, nightWindow);
        }
    }

    IEnumerator secondTimer()
    {
        yield return new WaitForSeconds(1);
        GlobalVars.timeMinutes += 1;
        GlobalVars.timeHourshly += 0.01667f;
        StartCoroutine(secondTimer());
    }

    public void ResetTime()
    {
        GlobalVars.timeHours = timeHours;
        GlobalVars.timeMinutes = timeMinutes;
        GlobalVars.amPm = amPm;
        GlobalVars.timeHourshly = timeHourshly;
        GlobalVars.multiplierForTime = multiplierForTime;
        GlobalVars.addTime = addTime;
        GlobalVars.extraAmPmTest = extraAmPmTest;
        GlobalVars.DayTime = DayTime;
    }
}
