using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AxeClass: MonoBehaviour
{
    //Note for lucky events; At the start of the game you can only possibly get the Normal Event or Lucky1.
    //After getting Lucky1 twice, Lucky1 becomes unavailable and you can start getting Lucky2 instead, at a lower chance.
    //After getting Lucky2 twice, Lucky2 becomes unavailable and you can start getting Lucky3 instead at a lower chance.
    //After getting Lucky3 once, it becomes unavailable and you can only get Normal events for the remainder of the game.
    //Lucky3 Unlocks the secret axe recipe, Victory's Secret.
    //After getting Lucky3, the text for Normal event changes. The effect of the Normal event does not change.
    //At Start:           Normal Event: 60%     Lucky1: 40%
    //After Lucky1 twice: Normal Event: 70%     Lucky2: 30%
    //After Lucky2 twice: Normal Event: 80%     Lucky3: 20%
    //After Lucky3 once:  Normal Event: 100%

    public CharacterStats player; //references the player stats
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    public string axeName;             //axe name
    public int power;               //axe power
    public float critMult;            //axe critical damage multiplier
    public float critRate;            //axe critical hit rate %
    public int staminaCost;         //stamina cost to use axe
    public int eventMarker = 0;        //keeps track of how far along the player is to unlocking Victory's Secret axe
    public GameObject VS;                   //references the crafting button for Victory's Secret

    public TextMeshProUGUI axeText; // text for what axe is equipped

    private string normalMessage1 = "You spent the day practicing crafting different tools, trying to find what does and doesn't work best. +1 Crafting!";
    private string normalMessage2 = "Having become a master of your craft, you spend the day practicing to stay sharp. +1 Crafting!";
    private string luckyMessage1 = "The nearby town's blacksmith stops by and watches you craft. Seeing your potential, he teaches you a few tricks. +2 Crafting!";
    private string luckyMessage2 = "The blacksmith stops by to support you with some of his materials for you to craft with. With some guidance, you improve! +2 Crafting! ";
    private string luckyMessage3 = "The blacksmith comes to you with an old, thin book. He mentions he has nothing left to teach you and suggests trying to craft his highest " +
        "quality axe, its recipe inside the book. +2 Crafting! You can now craft Victory's Secret!";

    void Start()
    {
        axeName = "Wooden Axe";
        power = 3;
        critMult = 2;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: Wooden Axe";
    }

    public void playPracticeCraftEvent()
    {
        //check if player has necessary stamina to perform action - if not, don't continue event
        if (player.stamina < 14)
        {
            messageBoxText.text = "Not enough Stamina to practice crafting today! You should consider resting.";
            return;
        }
        else
        {
            player.stamina -= 14;
            if (player.stamina < 0)
            {
                player.stamina = 0;
            }
        }

        int randnum = Random.Range(1, 101);
        Debug.Log(randnum);

        //Events that can play depending on the number of lucky events that were encountered - each lucky events progresses the eventMarker int
        switch (eventMarker)
        {
            case 0: case 1:
                if(randnum <= 60)
                {
                    normalEvent();
                }
                else
                {
                    eventMarker++;
                    lucky1();
                }
                break;
            case 2: case 3:
                if(randnum <= 70)
                {
                    normalEvent();
                }
                else
                {
                    eventMarker++;
                    lucky2();
                }
                break;
            case 4:
                if(randnum <= 80)
                {
                    normalEvent();
                }
                else
                {
                    eventMarker++;
                    lucky3();
                }
                break;
            case 5:
                normalEvent();
                break;
        }
    }

    public void normalEvent()
    {
        if(eventMarker != 5)
        {
            messageBoxText.text = normalMessage1;
        }
        else if (eventMarker == 5)
        {
            messageBoxText.text = normalMessage2;
        }
        player.crafting++;
    }

    public void lucky1()
    {
        messageBoxText.text = luckyMessage1;
        player.crafting += 2;
    }

    public void lucky2()
    {
        int randnum = Random.Range(1, 4);
        messageBoxText.text = luckyMessage2;
        switch (randnum)
        {
            case 1:
                messageBoxText.text += "+3 Iron!";
                player.iron += 3;
                break;
            case 2:
                messageBoxText.text += "+3 Gold!";
                player.gold += 3;
                break;
            case 3:
                messageBoxText.text += "+3 Titanium!";
                player.titanium += 3;
                break;
        }
        player.crafting += 2;
    }

    public void lucky3()
    {
        messageBoxText.text = luckyMessage3;
        player.crafting += 2;
        VS.SetActive(true);
    }

    public void equipStoneSplitter()
    {
        axeName = "Stone Splitter";
        power = 5;
        critMult = 2;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: Stone Splitter";
    }

    public void equipOlReliable()
    {
        axeName = "Ol' Reliable";
        power = 7;
        critMult = 2;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: Ol' Reliable";
    }

    public void equipIronMichael()
    {
        axeName = "Iron Michael";
        power = 9;
        critMult = 2;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: Iron Michael";
    }

    public void equipGoldDust()
    {
        axeName = "Gold Dust";
        power = 11;
        critMult = 2;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: Gold Dust";
    }

    public void equipTitaniumToppler()
    {
        axeName = "Titanium Toppler";
        power = 13;
        critMult = 2;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: Titanium Toppler";
    }

    public void equipHeavyHoward()
    {
        axeName = "Heavy Howard";
        power = 18;
        critMult = 2;
        critRate = 20;
        staminaCost = 20;
        axeText.text = "Axe: Heavy Howard";
    }

    public void equipEndAllBeAll()
    {
        axeName = "End All Be All";
        power = 28;
        critMult = 2;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: End All Be All";
    }

    public void equipCriticalImpact()
    {
        axeName = "Critical Impact";
        power = 21;
        critMult = 2;
        critRate = 0; //CHANGE LATER - THIS NUMBER CHANGES DPENEDING ON WHAT STR STAT IS UPON CRAFTING THIS AXE
        staminaCost = 20;
        axeText.text = "Axe: Critical Impact";
    }

    public void equipLumberAbuser()
    {
        axeName = "Lumber Abuser";
        power = 20;
        critMult = 4;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: Lumber Abuser";
    }

    public void equipFeatherweightEdge()
    {
        axeName = "Featherweight Edge";
        power = 20;
        critMult = 2;
        critRate = 10;
        staminaCost = 8;
        axeText.text = "Axe: Featherweight Edge";
    }

    //secret axe - only available when practicing crafting and the rare event of a blacksmith visiting appears
    //an event - the strongest axe available except a high crit rate critical impact
    //cost: craft level of 2, 30 of all materials, chesthair must be gorilla
    public void equipVictorysSecret()
    {
        axeName = "Victory's Secret";
        power = 30;
        critMult = 3;
        critRate = 20;
        staminaCost = 15;
        axeText.text = "Axe: Victory's Secret";
    }
}
