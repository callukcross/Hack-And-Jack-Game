using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AxeClass : MonoBehaviour
{
    public CharacterStats player; //references the player stats
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    public string axeName;             //axe name
    public int power;               //axe power
    public float critMult;            //axe critical damage multiplier
    public float critRate;            //axe critical hit rate %
    public int staminaCost;         //stamina cost to use axe

    public TextMeshProUGUI axeText; // text for what axe is equipped

    void Start()
    {
        axeName = "Wooden Axe";
        power = 3;
        critMult = 2;
        critRate = 10;
        staminaCost = 20;
        axeText.text = "Axe: Wooden Axe";
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