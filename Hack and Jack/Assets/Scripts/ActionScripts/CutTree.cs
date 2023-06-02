using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutTree : MonoBehaviour
{
    public CharacterStats player; //references the player stats
    public AxeManager currentAxe;  //references the currently equipped axe
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    int find;   // a dummy variable used to help find the index of the currently equipped axe

    int[] damageRange = new int[] { 0, 0 }; //array that determines damage ranges
    double baseDamage;  //damage before applying other factors
    double finalDamage;  //final damage to inflict

    private string normalMessage = "You swung at the tree all day long, inflicting ";
    private string critMessage = "You mercilessly assaulted the tree today, inflicting ";
    
    public void playCutEvent() //event that plays to cut tree and calculate damage
    {
        //check player has enough stamina to perform action
        if(player.stamina < 20)
        {
            messageBoxText.text = "Not enough Stamina to cut the tree today! You should go and rest.";
            return;
        }
        else
        {
            player.stamina -= 20;
            if(player.stamina < 0)
            {
                player.stamina = 0;
            }
        }

        for(int i = 0; i < currentAxe.axeList.Count; i++)   //looks through entire list of owned axes and assigns the find value the index of the currently equipped axe
        {
            if(currentAxe.axeList[i].isEquipped == true)
            {
                find = i;
            }
        }

        if(player.chesthair > 0 && player.chesthair <= 7)           //player chesthair stat will determine the possible damage range for each attack. Starts at a range from -10% to 10%
        {                                                           //at the beginning and when maxed out the range is -5% to 15%. This ensures slight increase in average damage as game
            damageRange[0] = -10;                                   //continues
            damageRange[1] = 10;
        }
        else if(player.chesthair >= 8 && player.chesthair <= 14)
        {
            damageRange[0] = -9;
            damageRange[1] = 11;
        }
        else if(player.chesthair >= 15 && player.chesthair <= 21)
        {
            damageRange[0] = -8;
            damageRange[1] = 12;
        }
        else if(player.chesthair >= 22 && player.chesthair <= 29)
        {
            damageRange[0] = -7;
            damageRange[1] = 13;
        }
        else if(player.chesthair >= 30)
        {
            damageRange[0] = -5;
            damageRange[1] = 15;
        }

        player.chesthair++; //increases chesthair stat for performing action
        daytree.day++;  //increases day count for performing action

        baseDamage = ((player.strength * 5.5) + (currentAxe.axeList[find].power * 4.5)) / 2;  // first formula for base damage - used as the base before other calculations
        double damageRangeRoll = Random.Range(damageRange[0], damageRange[1] + 1); //rolls to see what the damage range will turn to be

        baseDamage = (baseDamage * (1 + (damageRangeRoll / 100)));  //applies the randomly selected damage range to the base damage

        int critRoll = Random.Range(1, 101);    //rolls to see if a critical hit happens
        if(critRoll <= currentAxe.axeList[find].critRate)   //used to multiply damage if a crit happens - else no crit mult is applied
        {
            finalDamage = baseDamage * currentAxe.axeList[find].critMult;
            messageBoxText.text = critMessage + finalDamage.ToString() + " Critical Damage!";
        }
        else
        {
            finalDamage = baseDamage;
            messageBoxText.text = normalMessage + finalDamage.ToString() + " Damage!";
        }

        daytree.currentTreeHP -= finalDamage;   //inflict damage to tree equal to final damage calculated
        if(daytree.currentTreeHP >= 0)
        {
            daytree.currentTreeHP = 0;
        }
    }

    public void gameEnd()   //event that plays to end game when tree health becomes 0
    {

    }
}
