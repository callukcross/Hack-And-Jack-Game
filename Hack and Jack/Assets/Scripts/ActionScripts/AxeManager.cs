using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Axes
{
    public string axeName;           //axe name
    public int power;               //axe power
    public float critMult;            //axe critical damage multiplier
    public float critRate;            //axe critical hit rate %
    public int staminaCost;         //stamina cost to use axe
    public bool isEquipped;         //equip status (false = not equipped, true = equipped)
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

    public Button[] AxeButtons = new Button[11];

    public List<Axes> axeList = new List<Axes>();       //create List for Axe Objects to be stored into

    void Start()
    {
        //add the Wooden Axe as the starting axe for the game.
        Axes woodenaxe = new Axes("Wooden Axe", 3, 2, 10, 20, true, 0);
        axeList.Add(woodenaxe);
        player.axeText.text = "Axe: " + axeList[0].axeName;
    }

    public void equipAxe(Axes equippedAxe)    //function to equip an axe
    {
        for(int i = 0; i < axeList.Count; i++)      //turns all axes in the list to unequipped first
        {
            axeList[i].isEquipped = false;
        }

        for(int i = 0; i < 11; i++)   // turns the button UI for all axe buttons white to show they're unequipped
        {
            AxeButtons[i].image.color = Color.white;
        }

        axeList[axeList.FindIndex(Axes => Axes.axeID == equippedAxe.axeID)].isEquipped = true;  //finds the index of the equipped axe and sets it to be equipped
        player.axeText.text = "Axe: " + equippedAxe.axeName;    //changes equip text
        AxeButtons[equippedAxe.axeID - 1].image.color = Color.yellow;   //changes the equipped axe's button UI to yellow to show it is equipped

    }

    public void craftStoneSplitter()
    {
        Axes stonesplitter = new Axes("Stone Splitter", 5, 2, 10, 20, false, 1);   //create axe object
        Axes temp = axeList.Find((x) => x.axeID == 1);     //create a dummy axe object to detect if the above object is already in the list or not

        if(temp == null)    //use when object hasnt been crafted yet
        {
            if(player.stone >= 9 && player.crafting >= 1)   //Checks the player meets the material and stat requirements
            {
                player.stone -= 9;      //subtract the materials needed
                if(player.stone < 0)    //a cautionary check to make sure material value doesn't drop below 0
                {
                    player.stone = 0;
                }
                axeList.Add(stonesplitter);     //If object is not in list, add to list.
                AxeButtons[0].image.color = Color.white;    // Changes the Button UI to be white, showing that the axe is unlocked
                Debug.Log("You crafted the Stone Splitter!");   //message that crafting was successful, change later!!!!!
            }
            else   //plays if the player doesn't meet requirements to craft object
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!"); // change later!!!!!
            }
        }
        else if(temp != null)   //when axe is already crafted, plays this function instead - calls and uses the equip axe function
        {
            equipAxe(stonesplitter);
        }
    }

    public void craftOlReliable()
    {
        Axes olreliable = new Axes("Ol' Reliable", 7, 2, 10, 20, false, 2);
        Axes temp = axeList.Find((x) => x.axeID == 2);

        if (temp == null)
        {
            if(player.stone >= 12 && player.copper >= 4 && player.crafting >= 3)
            {
                player.stone -= 12;
                player.copper -= 4;
                if(player.stone < 0)
                {
                    player.stone = 0;
                }
                if(player.copper < 0)
                {
                    player.copper = 0;
                }
                axeList.Add(olreliable);
                AxeButtons[1].image.color = Color.white;
                Debug.Log("You crafted Ol' Reliable!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(olreliable);
        }
    }

    public void craftIronMichael()
    {
        Axes ironmichael = new Axes("Iron Michael", 9, 2, 10, 20, false, 3);
        Axes temp = axeList.Find((x) => x.axeID == 3);

        if (temp == null)
        {
            if(player.stone >= 20 && player.copper >= 10 && player.iron >= 6 && player.crafting >= 6)
            {
                player.stone -= 20;
                player.copper -= 10;
                player.iron -= 6;
                if(player.stone < 0)
                {
                    player.stone = 0;
                }
                if(player.copper < 0)
                {
                    player.copper = 0;
                }
                if(player.iron < 0)
                {
                    player.iron = 0;
                }
                axeList.Add(ironmichael);
                AxeButtons[2].image.color = Color.white;
                Debug.Log("You crafted the Iron Michael!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(ironmichael);
        }
    }

    public void craftGoldDust()
    {
        Axes golddust = new Axes("Gold Dust", 11, 2, 10, 20, false, 4);
        Axes temp = axeList.Find((x) => x.axeID == 4);

        if (temp == null)
        {
            if(player.stone >= 26 && player.copper >= 12 && player.iron >= 9 && player.gold >= 3 && player.crafting >= 10)
            {
                player.stone -= 26;
                player.copper -= 12;
                player.iron -= 9;
                player.gold -= 3;
                if (player.stone < 0)
                {
                    player.stone = 0;
                }
                if (player.copper < 0)
                {
                    player.copper = 0;
                }
                if (player.iron < 0)
                {
                    player.iron = 0;
                }
                if(player.gold < 0)
                {
                    player.gold = 0;
                }
                axeList.Add(golddust);
                AxeButtons[3].image.color = Color.white;
                Debug.Log("You crafted the Gold Dust!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(golddust);
        }
    }

    public void craftTitaniumToppler()
    {
        Axes titaniumtoppler = new Axes("Titanium Toppler", 13, 2, 10, 20, false, 5);
        Axes temp = axeList.Find((x) => x.axeID == 5);

        if (temp == null)
        {
            if(player.stone >= 34 && player.copper >= 15 && player.iron >= 12 && player.gold >= 9 && player.titanium >= 3 && player.crafting >= 12)
            {
                player.stone -= 34;
                player.copper -= 15;
                player.iron -= 12;
                player.gold -= 9;
                player.titanium -= 3;
                if (player.stone < 0)
                {
                    player.stone = 0;
                }
                if (player.copper < 0)
                {
                    player.copper = 0;
                }
                if (player.iron < 0)
                {
                    player.iron = 0;
                }
                if (player.gold < 0)
                {
                    player.gold = 0;
                }
                if(player.titanium < 0)
                {
                    player.titanium = 0;
                }
                axeList.Add(titaniumtoppler);
                AxeButtons[4].image.color = Color.white;
                Debug.Log("You crafted the Titanium Toppler!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(titaniumtoppler);
        }
    }

    public void craftHeavyHoward()
    {
        Axes heavyhoward = new Axes("Heavy Howard", 18, 2, 20, 20, false, 6);
        Axes temp = axeList.Find((x) => x.axeID == 6);

        if (temp == null)
        {
            if(player.stone >= 48 && player.copper >= 30 && player.iron >= 22 && player.gold >= 14 && player.titanium >= 9 && player.crafting >= 12 && player.strength >= 15)
            {
                player.stone -= 48;
                player.copper -= 30;
                player.iron -= 22;
                player.gold -= 14;
                player.titanium -= 9;
                if (player.stone < 0)
                {
                    player.stone = 0;
                }
                if (player.copper < 0)
                {
                    player.copper = 0;
                }
                if (player.iron < 0)
                {
                    player.iron = 0;
                }
                if (player.gold < 0)
                {
                    player.gold = 0;
                }
                if (player.titanium < 0)
                {
                    player.titanium = 0;
                }
                axeList.Add(heavyhoward);
                AxeButtons[5].image.color = Color.white;
                Debug.Log("You crafted the Heavy Howard!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(heavyhoward);
        }
    }

    public void craftEndAllBeAll()
    {
        Axes endallbeall = new Axes("The End All Be All", 28, 2, 10, 20, false, 7);
        Axes temp = axeList.Find((x) => x.axeID == 7);
        if (temp == null)
        {
            if (player.stone >= 80 && player.copper >= 67 && player.iron >= 55 && player.gold >= 35 && player.titanium >= 23 && player.crafting >= 28)
            {
                player.stone -= 80;
                player.copper -= 67;
                player.iron -= 55;
                player.gold -= 35;
                player.titanium -= 23;
                if (player.stone < 0)
                {
                    player.stone = 0;
                }
                if (player.copper < 0)
                {
                    player.copper = 0;
                }
                if (player.iron < 0)
                {
                    player.iron = 0;
                }
                if (player.gold < 0)
                {
                    player.gold = 0;
                }
                if (player.titanium < 0)
                {
                    player.titanium = 0;
                }
                axeList.Add(endallbeall);
                AxeButtons[6].image.color = Color.white;
                Debug.Log("You crafted The End All Be All!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(endallbeall);
        }
    }

    public void craftCriticalImpact()
    {
        Axes criticalimpact = new Axes("Critical Impact", 21, 2, 0, 20, false, 8); //CHANGE CRIT RATE LATER - THIS NUMBER CHANGES DPENEDING ON WHAT STR STAT IS UPON CRAFTING THIS AXE
        Axes temp = axeList.Find((x) => x.axeID == 8);

        if (temp == null)
        {
            if(player.iron >= 60 && player.gold >= 40 && player.titanium >= 20 && player.crafting >= 22 && player.strength >= 10)
            {
                player.iron -= 60;
                player.gold -= 40;
                player.titanium -= 20;
                if(player.iron < 0)
                {
                    player.iron = 0;
                }
                if(player.gold < 0)
                {
                    player.gold = 0;
                }
                if(player.titanium < 0)
                {
                    player.titanium = 0;
                }

                criticalimpact.critRate = 10 + player.strength; //adds strength stat to base crit rate
                if(criticalimpact.critRate > 100)   //prevents crit rate from going above 100%
                {
                    criticalimpact.critRate = 100;
                }

                axeList.Add(criticalimpact);
                AxeButtons[7].image.color = Color.white;
                Debug.Log("You crafted the Critical Impact!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(criticalimpact);
        }
    }

    public void craftLumberAbuser()
    {
        Axes lumberabuser = new Axes("Lumber Abuser", 20, 4, 10, 20, false, 9);
        Axes temp = axeList.Find((x) => x.axeID == 9);

        if (temp == null)
        {
            if(player.copper >= 75 && player.iron >= 5 && player.crafting >= 16)
            {
                player.copper -= 75;
                player.iron -= 5;
                if(player.copper < 0)
                {
                    player.copper = 0;
                }
                if(player.iron < 0)
                {
                    player.iron = 0;
                }
                axeList.Add(lumberabuser);
                AxeButtons[8].image.color = Color.white;
                Debug.Log("You crafted the Lumber Abuser!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(lumberabuser);
        }
    }

    public void craftFeatherweightEdge()
    {
        Axes featherweightedge = new Axes("Featherweight Edge", 20, 2, 10, 8, false, 10);
        Axes temp = axeList.Find((x) => x.axeID == 10);

        if (temp == null)
        {
            if(player.iron >= 2 && player.gold >= 2 && player.titanium >= 3 && player.crafting >= 18)
            {
                player.iron -= 2;
                player.gold -= 2;
                player.titanium -= 3;
                if(player.iron < 0)
                {
                    player.iron = 0;
                }
                if(player.gold < 0)
                {
                    player.gold = 0;
                }
                if(player.titanium < 0)
                {
                    player.titanium = 0;
                }
                axeList.Add(featherweightedge);
                AxeButtons[9].image.color = Color.white;
                Debug.Log("You crafted the Featherweight Edge!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(featherweightedge);
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
            if (player.stone >= 30 && player.copper >= 30 && player.iron >= 30 && player.gold >= 30 && player.titanium >= 30 && player.crafting >= 1 && player.chesthair >= 30)
            {
                player.stone -= 30;
                player.copper -= 30;
                player.iron -= 30;
                player.gold -= 30;
                player.titanium -= 30;
                if (player.stone < 0)
                {
                    player.stone = 0;
                }
                if (player.copper < 0)
                {
                    player.copper = 0;
                }
                if (player.iron < 0)
                {
                    player.iron = 0;
                }
                if (player.gold < 0)
                {
                    player.gold = 0;
                }
                if (player.titanium < 0)
                {
                    player.titanium = 0;
                }
                axeList.Add(victoryssecret);
                AxeButtons[10].image.color = Color.white;
                Debug.Log("You crafted Victory's Secret!");
            }
            else
            {
                Debug.Log("You don't have enough materials, or the right stat requirements to craft this item!");
            }
        }
        else if (temp != null)
        {
            equipAxe(victoryssecret);
        }
    }
}

