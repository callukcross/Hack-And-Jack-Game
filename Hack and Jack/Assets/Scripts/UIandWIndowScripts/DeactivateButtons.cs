using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateButtons : MonoBehaviour
{
    public Button[] actionButtons = new Button[7];
    public Button[] axeButtons = new Button[12];

    public void deactivateActions()
    {
        int i;
        for(i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].enabled = false;
        }
    }

    public void activateAxes()
    {
        int i;
        for (i = 0; i < axeButtons.Length; i++)
        {
            axeButtons[i].enabled = true;
        }
    }

    public void deactivateAxes()
    {
        int i;
        for (i = 0; i < axeButtons.Length; i++)
        {
            axeButtons[i].enabled = false;
        }

    }

    public void activateActions()
    {
        int i;
        for(i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].enabled = true;
        }
    }
}
