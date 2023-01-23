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

    }

    public void olReliable()
    {

    }

    public void ironMike()
    {

    }

    public void goldDust()
    {

    }

    public void titaniumToppler()
    {

    }

    public void heavyHoward()
    {

    }

    public void endAllBeAll()
    {

    }

    public void criticalImpact()
    {

    }

    public void lumberAbuser()
    {

    }

    public void featherweightEdge()
    {

    }
}
