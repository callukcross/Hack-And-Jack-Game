using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionPivot : MonoBehaviour
{
    public bool MainScreen;
    public bool StatsScreen;
    public bool AxeScreen;
    public bool SettingScreen;

    public void Start()
    {
        MainScreen = true;
    }

    public void pivotSwitchToMain()
    {
        if (MainScreen == false)
        {
            MainScreen = true;
            StatsScreen = false;
            AxeScreen = false;
            SettingScreen = false;
        }
    }

    public void pivotSwitchToStats()
    {
        if (StatsScreen == false)
        {
            MainScreen = false;
            StatsScreen = true;
            AxeScreen = false;
            SettingScreen = false;
        }
    }

    public void pivotSwitchToAxe()
    {
        if (AxeScreen == false)
        {
            MainScreen = false;
            StatsScreen = false;
            AxeScreen = true;
            SettingScreen = false;
        }
    }

    public void pivotSwitchToSetting()
    {
        if (SettingScreen == false)
        {
            MainScreen = false;
            StatsScreen = false;
            AxeScreen = false;
            SettingScreen = true;
        }
    }
}
