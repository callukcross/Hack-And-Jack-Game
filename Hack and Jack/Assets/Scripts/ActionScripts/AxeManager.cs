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

    //Constructor to initiate the list - prevents 'object reference not set to instance' error
    public Axes(string _axeName, int _power, float _critMult, float _critRate, int _staminaCost)
    {
        axeName = _axeName;
        power = _power;
        critMult = _critMult;
        critRate = _critRate;
        staminaCost = _staminaCost;
    }
}

public class AxeManager : MonoBehaviour
{
    public CharacterStats player; //references the player stats
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    public List<Axes> axeList = new List<Axes>();

    void Start()
    {
        //add the Wooden Axe as the starting axe for the game.
        Axes woodenaxe = new Axes("Wooden Axe", 3, 2, 10, 20);
        axeList.Add(woodenaxe);
    }

    void StoneSplitter()
    {
        Axes stonesplitter = new Axes("Stone Splitter", 5, 2, 10, 20);
        axeList.Add(stonesplitter);
    }

    void OlReliable()
    {
        Axes olreliable = new Axes("Ol' Reliable", 7, 2, 10, 20);
        axeList.Add(olreliable);
    }

    void IronMichael()
    {
        Axes ironmichael = new Axes("Iron Michael", 9, 2, 10, 20);
        axeList.Add(ironmichael);
    }

    void GoldDust()
    {
        Axes golddust = new Axes("Gold Dust", 11, 2, 10, 20);
        axeList.Add(golddust);
    }

    void TitaniumToppler()
    {
        Axes titaniumtoppler = new Axes("Titanium Toppler", 13, 2, 10, 20);
        axeList.Add(titaniumtoppler);
    }

    void HeavyHoward()
    {
        Axes heavyhoward = new Axes("Heavy Howard", 18, 2, 20, 20);
        axeList.Add(heavyhoward);
    }

    void EndAllBeAll()
    {
        Axes endallbeall = new Axes("The End All Be All", 28, 2, 10, 20);
        axeList.Add(endallbeall);
    }

    void CriticalImpact()
    {
        Axes criticalimpact = new Axes("Critical Impact", 21, 2, 0, 20); //CHANGE CRIT RATE LATER - THIS NUMBER CHANGES DPENEDING ON WHAT STR STAT IS UPON CRAFTING THIS AXE
        axeList.Add(criticalimpact);
    }

    void LumberAbuser()
    {
        Axes lumberabsuer = new Axes("Lumber Abuser", 20, 4, 10, 20);
        axeList.Add(lumberabsuer);
    }

    void FeatherweightEdge()
    {
        Axes featherweightedge = new Axes("Featherweight Edge", 20, 2, 10, 8);
        axeList.Add(featherweightedge);
    }

    //secret axe - only available when practicing crafting and the rare event of a blacksmith visiting appears
    //an event - the strongest axe available except a high crit rate critical impact
    //cost: craft level of 2, 30 of all materials, chesthair must be gorilla
    void VictorysSecret()
    {
        Axes victoryssecret = new Axes("Victory's Secret", 30, 3, 20, 15);
        axeList.Add(victoryssecret);
    }
}

