using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Axes
{
    public string axeName;             //axe name
    public int power;               //axe power
    public float critMult;            //axe critical damage multiplier
    public float critRate;            //axe critical hit rate %
    public int staminaCost;         //stamina cost to use axe
    public bool isEquipped;         //equip status (0 = not equipped, 1 = equipped)
    public int axeID;               //identification number for each axe object in list

    //Constructor to initiate the list - prevents 'object reference not set to instance' error
    public Axes(string _axeName, int _power, float _critMult, float _critRate, int _staminaCost, bool _isEquipped, int _axeID)
    {
        axeName = _axeName;
        power = _power;
        critMult = _critMult;
        critRate = _critRate;
        staminaCost = _staminaCost;
        isEquipped = _isEquipped;
        axeID = _axeID;
    }
}

public class AxeManager : MonoBehaviour
{
    public CharacterStats player; //references the player stats
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    public List<Axes> axeList = new List<Axes>();       //create List for Axe Objects to be stored into

    void Start()
    {
        //add the Wooden Axe as the starting axe for the game. Adds a bool of the same location number to track if locked - on default, false, not locked
        Axes woodenaxe = new Axes("Wooden Axe", 3, 2, 10, 20, true, 0);
        axeList.Add(woodenaxe);
    }

    public void craftStoneSplitter()
    {
        Axes stonesplitter = new Axes("Stone Splitter", 5, 2, 10, 20, false, 1);   //create axe object
        Axes temp = axeList.Find((x) => x.axeID == 1);     //create a dummy axe object to detect if the above object is already in the list or not

        if(temp == null)
        {
            Debug.Log("No");
            axeList.Add(stonesplitter);     //If object is not in list, add to list.
        }
        else if(temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftOlReliable()
    {
        Axes olreliable = new Axes("Ol' Reliable", 7, 2, 10, 20, false, 2);
        Axes temp = axeList.Find((x) => x.axeID == 2);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(olreliable);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftIronMichael()
    {
        Axes ironmichael = new Axes("Iron Michael", 9, 2, 10, 20, false, 3);
        Axes temp = axeList.Find((x) => x.axeID == 3);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(ironmichael);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftGoldDust()
    {
        Axes golddust = new Axes("Gold Dust", 11, 2, 10, 20, false, 4);
        Axes temp = axeList.Find((x) => x.axeID == 4);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(golddust);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftTitaniumToppler()
    {
        Axes titaniumtoppler = new Axes("Titanium Toppler", 13, 2, 10, 20, false, 5);
        Axes temp = axeList.Find((x) => x.axeID == 5);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(titaniumtoppler);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftHeavyHoward()
    {
        Axes heavyhoward = new Axes("Heavy Howard", 18, 2, 20, 20, false, 6);
        Axes temp = axeList.Find((x) => x.axeID == 6);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(heavyhoward);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftEndAllBeAll()
    {
        Axes endallbeall = new Axes("The End All Be All", 28, 2, 10, 20, false, 7);
        Axes temp = axeList.Find((x) => x.axeID == 7);
        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(endallbeall);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftCriticalImpact()
    {
        Axes criticalimpact = new Axes("Critical Impact", 21, 2, 0, 20, false, 8); //CHANGE CRIT RATE LATER - THIS NUMBER CHANGES DPENEDING ON WHAT STR STAT IS UPON CRAFTING THIS AXE
        Axes temp = axeList.Find((x) => x.axeID == 8);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(criticalimpact);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftLumberAbuser()
    {
        Axes lumberabuser = new Axes("Lumber Abuser", 20, 4, 10, 20, false, 9);
        Axes temp = axeList.Find((x) => x.axeID == 9);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(lumberabuser);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    public void craftFeatherweightEdge()
    {
        Axes featherweightedge = new Axes("Featherweight Edge", 20, 2, 10, 8, false, 10);
        Axes temp = axeList.Find((x) => x.axeID == 10);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(featherweightedge);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }

    //secret axe - only available when practicing crafting and the rare event of a blacksmith visiting appears
    //an event - the strongest axe available except a high crit rate critical impact
    //cost: craft level of 2, 30 of all materials, chesthair must be gorilla
    public void craftVictorysSecret()
    {
        Axes victoryssecret = new Axes("Victory's Secret", 30, 3, 20, 15, false, 11);
        Axes temp = axeList.Find((x) => x.axeID == 11);

        if (temp == null)
        {
            Debug.Log("No");
            axeList.Add(victoryssecret);
        }
        else if (temp != null)
        {
            Debug.Log("Yes");
        }
    }
}

