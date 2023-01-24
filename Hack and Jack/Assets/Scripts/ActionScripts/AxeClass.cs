using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AxeClass: MonoBehaviour
{
    public string axeName;             //axe name
    public int power;               //axe power
    public float critMult;            //axe critical damage multiplier
    public float critRate;            //axe critical hit rate multiplier
    public int staminaCost;         //stamina cost to use axe
    public int motivationCost;      //motivation cost to use axe
    public int status;              // status of axe. 0 = locked, 1 = unlocked, 2 = equipped

    public TextMeshProUGUI axeText; // text for what axe is equipped

    void Start()
    {
        axeName = "Wooden Axe";
        power = 3;
        critMult = 2;
        critRate = 1;
        staminaCost = 20;
        motivationCost = 20;
        status = 2;
        axeText.text = "Axe: Wooden Axe";
    }

    public void stoneSplitter()
    {
        axeName = "Stone Splitter";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Stone Splitter";
    }

    public void olReliable()
    {
        axeName = "Ol' Reliable";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Ol' Reliable";
    }

    public void ironMike()
    {
        axeName = "Iron Michael";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Iron Michael";
    }

    public void goldDust()
    {
        axeName = "Gold Dust";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Gold Dust";
    }

    public void titaniumToppler()
    {
        axeName = "Titanium Toppler";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Titanium Toppler";
    }

    public void heavyHoward()
    {
        axeName = "Heavy Howard";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Heavy Howard";
    }

    public void endAllBeAll()
    {
        axeName = "End All Be All";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: End All Be All";
    }

    public void criticalImpact()
    {
        axeName = "Critical Impact";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Critical Impact";
    }

    public void lumberAbuser()
    {
        axeName = "Lumber Abuser";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Lumber Abuser";
    }

    public void featherweightEdge()
    {
        axeName = "Featherweight Edge";
        power = 0;
        critMult = 0;
        critRate = 0;
        staminaCost = 0;
        motivationCost = 0;
        status = 0;
        axeText.text = "Axe: Featherweight Edge";
    }
}
