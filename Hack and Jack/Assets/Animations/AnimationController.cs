using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MessageWindowControl
{
    Animator animate;
    public transitionPivot currentScreen;

    void Start()
    {
        animate = gameObject.GetComponent<Animator>();
    }

    public void buttonsSlideOut()
    {
        animate.SetTrigger("SlideOut");
    }

    public void buttonsSlideIn()
    {
        animate.SetTrigger("SlideIn");
    }

    public void messageBoxOut()
    {
        animate.SetTrigger("Activate");
    }

    public void MainToStatsEnter()
    {
        if(currentScreen.MainScreen == true)
        {
            animate.SetTrigger("CameraMainToStatsEnter");
        }
    }

    public void StatsToMainEnter()
    {
        if(currentScreen.StatsScreen == true)
        {
            animate.SetTrigger("CameraStatsToMainEnter");
        }
    }

    public void MainToAxeEnter()
    {
        if(currentScreen.MainScreen == true)
        {
            animate.SetTrigger("CameraMainToAxeEnter");
        }
    }

    public void AxeToMainEnter()
    {
        if (currentScreen.AxeScreen == true)
        {
            animate.SetTrigger("CameraAxeToMainEnter");
        }
    }

    public void MainToSettingsEnter()
    {
        if (currentScreen.MainScreen == true)
        {
            animate.SetTrigger("CameraMainToSettingsEnter");
        }
    }

    public void SettingsToMainEnter()
    {
        if (currentScreen.SettingScreen == true)
        {
            animate.SetTrigger("CameraSettingsToMainEnter");
        }
    }
}
