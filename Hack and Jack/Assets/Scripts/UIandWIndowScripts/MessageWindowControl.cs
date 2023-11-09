using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindowControl : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvas2;
    public GameObject messageWindow;


    public void gameWindowEnter()
    {
        canvas.SetActive(true);
        messageWindow.SetActive(true);
    }

    public void gameWindowExit()
    {
        canvas.SetActive(false);
        messageWindow.SetActive(false);
    }

    public void actionsToAxeCrafting()
    {
        canvas2.SetActive(true);
        canvas.SetActive(false);
    }

    public void axeCraftingToActions()
    {
        canvas.SetActive(true);
        canvas2.SetActive(false);
    }
}
