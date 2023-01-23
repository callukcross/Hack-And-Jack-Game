using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindowControl : MonoBehaviour
{
    public GameObject canvas;
    public GameObject messageWindow;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public Button button10;
    public Button button11;
    public Button button12;
    public float delayTime;
    public transitionPivot currentScreens;
    public GameObject ToMainButton;
    public GameObject ToStatsButton;

    IEnumerator delayTimer1()
    {
        yield return new WaitForSeconds(delayTime);
        ToMainButton.SetActive(true);
        ToStatsButton.SetActive(false);
        button9.interactable = true;
    }

    IEnumerator delayTimer2()
    {
        yield return new WaitForSeconds(delayTime);
        ToStatsButton.SetActive(true);
        ToMainButton.SetActive(false);
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
        button4.interactable = true;
        button5.interactable = true;
        button6.interactable = true;
        button7.interactable = true;
        button8.interactable = true;
        button11.interactable = true;
        button12.interactable = true;
    }

    IEnumerator delayTimer3()
    {
        yield return new WaitForSeconds(delayTime);
        button10.interactable = true;
    }

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

    public void deactivateButtons()
    {
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
        button5.interactable = false;
        button6.interactable = false;
        button7.interactable = false;
        button8.interactable = false;
        button9.interactable = false;
        button10.interactable = false;
        button11.interactable = false;
        button12.interactable = false;
    }

    public void activateButtons()
    {
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
        button4.interactable = true;
        button5.interactable = true;
        button6.interactable = true;
        button7.interactable = true;
        button8.interactable = true;
        button9.interactable = true;
        button10.interactable = true;
        button11.interactable = true;
        button12.interactable = true;
    }

    public void screenTransitionButtonReactivate1()
    {
        StartCoroutine(delayTimer1());
    }

    public void screenTransitionButtonReactivate2()
    {
        StartCoroutine(delayTimer2());
    }

    public void screenTransitionButtonReactivate3()
    {
        StartCoroutine(delayTimer3());
    }
}
