using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public CharacterStats player; //references the player stats
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    private int gStamina; // value which the game generates stamina gain
    private int[] chesthairModifier = {0, 0, 0, 0, 0};

    private string normalMessage = "You slept through the night, resting body, mind and soul. +33 Stamina!"; //normal gain +33 stamina
    private string luckyMessage1 = "Your stretches before bed did wonders! You feel extra well rested today! +50 Stamina!"; //extra gain, +50 stamina
    private string luckyMessage2 = "You dreamed of mining valuable resources, and approached an epiphany for better mining strategies. +33 Stamina! +1 Mining!"; //normal gain, +1 mining
    private string luckyMessage3 = "Dreams of owning high end axes come to you. Upon waking, you feel motivated to make one yourself! +33 Stamina! +1 Crafting!"; //normal gain, +1 crafting
    private string luckyMessage4 = "You discovered a brand new natural hot spring before bed! Let all of your troubles melt away. Your stamina is fully restored!"; //extra gain, full stamina increase

    public void playRestEvent()
    {
        // checks that the player isn't already at full stamina and stops the program if they are
        if(player.stamina == 100)
        {
            messageBoxText.text = "You're already fully rested! You should do something else today.";
            return;
        }

        //increment day count
        daytree.day++;

        //using the chesthair stat, determine probability of events [0]normal, [1]lucky1, [2]lucky2, [3]lucky3, [4]lucky4
        switch(player.chesthair)
        {
            case 1: case 2: case 3: case 4: case 5: case 6: case 7:
                chesthairModifier[0] = 60;
                chesthairModifier[1] = 20;
                chesthairModifier[2] = 10;
                chesthairModifier[3] = 7;
                chesthairModifier[4] = 3;
                break;
            case 8: case 9: case 10: case 11: case 12: case 13: case 14:
                chesthairModifier[0] = 56;
                chesthairModifier[1] = 21;
                chesthairModifier[2] = 11;
                chesthairModifier[3] = 8;
                chesthairModifier[4] = 4;
                break;
            case 15: case 16: case 17: case 18:case 19: case 20: case 21:
                chesthairModifier[0] = 52;
                chesthairModifier[1] = 22;
                chesthairModifier[2] = 12;
                chesthairModifier[3] = 9;
                chesthairModifier[4] = 5;
                break;
            case 22: case 23: case 24: case 25: case 26: case 27: case 28: case 29:
                chesthairModifier[0] = 48;
                chesthairModifier[1] = 23;
                chesthairModifier[2] = 13;
                chesthairModifier[3] = 10;
                chesthairModifier[4] = 6;
                break;
            case int n when (n >= 30):
                chesthairModifier[0] = 44;
                chesthairModifier[1] = 24;
                chesthairModifier[2] = 14;
                chesthairModifier[3] = 11;
                chesthairModifier[4] = 7;
                break;
        }

        //use RNG to determine which event plays
        int randnum = Random.Range(1, 101);

        //blocks are used to help seperate event probabilities when rolling RNG - helps keep code cleaner
        int block1 = chesthairModifier[0];
        int block2 = block1 + chesthairModifier[1];
        int block3 = block2 + chesthairModifier[2];
        int block4 = block3 + chesthairModifier[3];
        int block5 = block4 + chesthairModifier[4];

        //pick event using RNG
        if(randnum <= block1)
        {
            normalEvent();
        }
        else if(randnum > block1 && randnum <= block2)
        {
            luckyEvent1();
        }
        else if(randnum > block2 && randnum <= block3)
        {
            luckyEvent2();
        }
        else if(randnum > block3 && randnum <= block4)
        {
            luckyEvent3();
        }
        else if(randnum > block4 && randnum <= block5)
        {
            luckyEvent4();
        }

        //functions that play events
        void normalEvent()
        {
            player.stamina += 33;
            if(player.stamina > player.maxStamina)
            {
                player.stamina = player.maxStamina;
            }
            messageBoxText.text = normalMessage;
        }

        void luckyEvent1()
        {
            player.stamina += 50;
            if (player.stamina > player.maxStamina)
            {
                player.stamina = player.maxStamina;
            }
            messageBoxText.text = luckyMessage1;
        }

        void luckyEvent2()
        {
            player.stamina += 33;
            player.mining++;
            if (player.stamina > player.maxStamina)
            {
                player.stamina = player.maxStamina;
            }
            messageBoxText.text = luckyMessage2;
        }

        void luckyEvent3()
        {
            player.stamina += 33;
            player.crafting++;
            if (player.stamina > player.maxStamina)
            {
                player.stamina = player.maxStamina;
            }
            messageBoxText.text = luckyMessage3;
        }

        void luckyEvent4()
        {
            player.stamina = player.maxStamina;
            messageBoxText.text = luckyMessage4;
        }


    }
}
