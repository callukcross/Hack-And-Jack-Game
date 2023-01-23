using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//NOTE: RETURN LATER TO IMPLEMENT CODE FOR OBTAINING LOUNGE PACKAGES AND SKINS - EVENTS 6 AND 7 RESPECTIVELY

public class Fishing : MonoBehaviour
{
    public CharacterStats player; //references the player stats
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    private int chooseEvent; //value to determine which event will be played

    //All messages for the events
    // unlucky is just +12 stamina
    // lucky1 is +12 stamina and +1 mining
    // lucky2 is +12 stamina and +1 crafting
    // lucky3 is +12 stamina and +1 strength
    // lucky4 is +12 stamina and extra chesthair gain for the day
    // lucky5 is +12 stamina and +2 for a random mineral
    // lucky6 is +12 stamina and +1 lounge package
    // lucky7 is +12 stamina and new skin unlocked
    private string unluckyMessage = "The fish don't seem to be active today, and nothing else was happening, leading to a small haul. +12 Stamina!";
    private string luckyMessage1 = "A group of miners exit the nearby cave and stop by to chat with you. After a long talk, you learned some caving advice. +12 Stamina! +1 Mining!";
    private string luckyMessage2 = "A blacksmith from the local town starts working on a bench not far. After striking up a conversation, you chat about your craft and" +
        " learn something new. +12 Stamina! +1 Crafting!";
    private string luckyMessage3 = "After some time, you hit the big one! After a long struggle, you pull up a giant carp! It made for both a good workout and a good " +
        "dinner. +12 Stamina! +1 Strength!";
    private string luckyMessage4 = "The sun is beating down on you hard today. Toughing it out and fishing for dinner really put some hair on your chest. +12 Stamina! Extra " +
        "Chesthair gain for today!";
    private string luckyMessage5 = "Your rod catches on something heavy. Reeling it in, you find there was some minerals caught on your rod! +12 Stamina!";
    private string luckyMessage6 = "A nearby boat seemingly drops a cargo shipment by mistake. Looking through it, you find a Lounge Package! Might as well keep it! +12 Stamina! " +
        "Gained 1 Lounge Package!";
    private string luckyMessage7 = "A dapper fellow notices you and your wet clothes. Handing you a suitcase, he wishes you the best of luck. Inside, you find a new outfit. +12 Stamina! " +
        "New skin unlocked!";

    public void playFishingEvent()
    {
        // checks that the player isn't already at full stamina and stops the program if they are
        if (player.stamina == 100)
        {
            messageBoxText.text = "You already have plenty of food and stamina, you should focus on something else today.";
            return;
        }

        //increment day count and chesthair, and increase stamina
        daytree.day++;
        player.chesthair++;
        player.stamina += 12;
        if(player.stamina > 100)
        {
            player.stamina = 100;
        }

        //use RNG to determine which event plays
        int randnum = Random.Range(1, 101);

        //blocks are used to help seperate event probabilities when rolling RNG - helps keep code cleaner
        int block1 = 9;
        int block2 = block1 + 13;
        int block3 = block2 + 13;
        int block4 = block3 + 13;
        int block5 = block4 + 13;
        int block6 = block5 + 13;
        int block7 = block6 + 13;
        int block8 = block7 + 13;

        //pick event using RNG
        if(randnum <= block1)
        {
            unluckyEvent();
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
        else if(randnum > block5 && randnum <= block6)
        {
            luckyEvent5();
        }
        else if(randnum > block6 && randnum <= block7)
        {
            luckyEvent6();
        }
        else if(randnum > block7 && randnum <= block8)
        {
            luckyEvent7();
        }


        //functions that play events
        void unluckyEvent()
        {
            messageBoxText.text = unluckyMessage;
        }

        void luckyEvent1()
        {
            messageBoxText.text = luckyMessage1;
            player.mining++;
        }

        void luckyEvent2()
        {
            messageBoxText.text = luckyMessage2;
            player.crafting++;
        }

        void luckyEvent3()
        {
            messageBoxText.text = luckyMessage3;
            player.strength++;
        }

        void luckyEvent4()
        {
            messageBoxText.text = luckyMessage4;
            player.chesthair++;
        }

        void luckyEvent5()
        {
            int randnum = Random.Range(0, 5);
            string ore = "";
            switch(randnum)
            {
                case 0:
                    ore = " +2 Stone!";
                    player.stone += 2;
                    break;
                case 1:
                    ore = " +2 Copper!";
                    player.copper += 2;
                    break;
                case 2:
                    ore = " +2 Iron!";
                    player.iron += 2;
                    break;
                case 3:
                    ore = " +2 Gold!";
                    player.gold += 2;
                    break;
                case 4:
                    ore = " +2 Titanium!";
                    player.titanium += 2;
                    break;
            }
            messageBoxText.text = luckyMessage5 + ore;
        }

        void luckyEvent6()
        {
            messageBoxText.text = luckyMessage6;
            //implement code for obtaining lounge package
        }

        void luckyEvent7()
        {
            messageBoxText.text = luckyMessage7;
            //implement code for obtaining new skin
        }
    }
}
