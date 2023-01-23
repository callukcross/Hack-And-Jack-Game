using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// NOTE: CODE NOT COMPLETE UNTIL SKINS ARE ADDED - MUST BE ABLE TO EARN SKIN THROUGH LUCKY EVENT 5 AN PLAYTEST THIS EVENT
// NOTE: REMOVE ALL DEBUG LOGS WHEN CODE IS FULLY COMPLETE AND TESTED

public class Mining : MonoBehaviour
{
    public CharacterStats player; //references the player stats
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    private int gStone;  //these are the values in which the game will randomly generate to add to the player's inventory
    private int gCopper;
    private int gIron;
    private int gGold;
    private int gTitanium;
    private int[] chesthairModifier = { 0, 0 }; //when rolling RNG for lucky and unlucky events, this value is determined by the player chesthair stat.
    private int[] chooseEvent = { 0, 0 } ; // [0] 0 = normal, 1 = lucky, 2 = unlucky    [1] specific event numbers
    private int miningTier; // this is determined by the player's mining stat and determines rewards given upon taking the mining action

    // All messages used for the message box for each event to notify the player of what happens
    // normal event gives normal rewards
    // lucky1 gives an extra +1 to mining along with normal rewards
    // lucky2 gives +1 strength along with normal rewards
    // lucky3 gives an extra +3 on the highest tier available mineral
    // lucky4 takes no stamina away for the day
    // lucky5 gives the player a new skin to equip along with normal rewards
    // unlucky1 takes away 1 of all materials earned that day (min earn is 0)
    // unlucky2 is double stamina loss for the day (min stamina is 0)
    // unlucky3 is -2 on highest tier mineral gained for the day (min earn is 0)
    // unlucky4 is no Mining gain for the day
    private string normalMessage = "You spend the day mining away at the nearby cavern and recieve your normal haul.";                                   //under chooseEvent {0,0} 
    private string luckyMessage1 = "You discover a brand new cave system, filled with many fresh veins of minerals, and new experiences to be had! You earned" +
        " an extra +1 to Mining today!";                                                                                                                 //under chooseEvent {1,1} 
    private string luckyMessage2 = "You managed to somehow move hundreds of pounds of heavy minerals without hurting yourself! +1 to Strength today!";   //under chooseEvent {1,2} 
    private string luckyMessage3 = "A kind old man notices you struggle, and offers you some of his own minerals as compensation for your hard work. " +
        "+3 on highest tier Mineral earned today!";                                                                                                //under chooseEvent {1,3} 
    private string luckyMessage4 = "All the minerals today were gathered in one place, making mining today much more efficient! " +
        "No Stamina loss for today!";                                                                                                                     //under chooseEvent {1,4} 
    private string luckyMessage5 = "While traveling deep into the earth, you find some strange and interesting gear. You decide to take it home! New skin unlocked!"; //under chooseEvent {1,5} 
    private string unluckyMessage1 = "Oh no! As you were leaving the cave, a group of bandits shown up and robbed you! -1 on all Minerals earned today...";   //under chooseEvent {2,1} 
    private string unluckyMessage2 = "You hurt yourself moving the heavy rocks and minerals today. Double Stamina loss for today...";                          //under chooseEvent {2,2} 
    private string unluckyMessage3 = "An earthquake hits, leading to a cave in that you barely escape, ending your mining trip early. -2 on highest tier " +
        "Mineral earned today...";                                                                                                                       //under chooseEvent {2,3} 
    private string unluckyMessage4 = "Your pickaxe broke, leaving you to spend the rest of the day mining by hand. No Mining gained today...";  //under chooseEvent {2,4} 

    public void playMiningEvent()
    {
        //check if player has necessary stamina to perform action - if yes, remove 18 stamina, if no, kill action. Also check to prevent underflow and going below 0 stamina
        if(player.stamina < 18)
        {
            messageBoxText.text = "Not enough Stamina to go mining today! You should consider resting.";
            return;
        }
        else
        {
            player.stamina -= 18;
            if(player.stamina < 0)
            {
                player.stamina = 0;
            }
        }

        //increment day count
        daytree.day++;

        //determine what mining tier the player is on according to their mining level - this determines reward outcome
        switch(player.mining)
        {
            case 1: case 2: case 3:
                miningTier = 1;
                break;
            case 4: case 5: case 6:
                miningTier = 2;
                break;
            case 7: case 8: case 9:
                miningTier = 3;
                break;
            case 10: case 11:
                miningTier = 4;
                break;
            case 12: case 13:
                miningTier = 5;
                break;
            case 14: case 15:
                miningTier = 6;
                break;
            case 16: case 17:
                miningTier = 7;
                break;
            case 18: case 19: case 20: case 21: case 22: case 23: case 24:
                miningTier = 8;
                break;
            case int n when (n >= 25):
                miningTier = 9;
                break;
        }

        //set probability for lucky/unlucky events using passed in chesthair stat. [0] is the % for lucky events to occur and [1] is the % for unlucky events to occur
        switch(player.chesthair)
        {
            case 1: case 2: case 3: case 4: case 5: case 6: case 7:
                chesthairModifier[0] = 20;
                chesthairModifier[1] = 20;
                break;
            case 8: case 9: case 10: case 11: case 12: case 13: case 14:
                chesthairModifier[0] = 21;
                chesthairModifier[1] = 19;
                break;
            case 15: case 16: case 17: case 18: case 19: case 20: case 21:
                chesthairModifier[0] = 22;
                chesthairModifier[1] = 18;
                break;
            case 22: case 23: case 24: case 25: case 26: case 27: case 28: case 29:
                chesthairModifier[0] = 23;
                chesthairModifier[1] = 17;
                break;
            case int n when (n >= 30):
                chesthairModifier[0] = 25;
                chesthairModifier[1] = 15;
                break;
        }

        //increase chesthair stat after determining probability as this action guarantees chesthair increase
        player.chesthair++;

        //rng to determine event type (lucky/unlucky/normal)
        int randnum = Random.Range(1, 101);
        if(randnum <= chesthairModifier[1]) //unlucky event check
        {
            chooseEvent[0] = 2; //unlucky event
        }
        else if(randnum >= (100 - chesthairModifier[0])) //lucky event check
        {
            chooseEvent[0] = 1; //lucky event
        }
        else //normal event check
        {
            chooseEvent[0] = 0; //normal event
        }

        //rng to determine which event plays after determining what type it is. case 0 (normal event) does not need rng because there is only 1 normal event to pick from
        switch(chooseEvent[0])
        {
            case 1:
                chooseEvent[1] = Random.Range(1, 6); //randomly picks lucky events 1-5.
                break;
            case 2:
                chooseEvent[1] = Random.Range(1, 5); //randomly picks unlucky events 1-4.
                break;
        }

        //call the event that was chosen by RNG above
        if(chooseEvent[0] == 1)
        {
            switch (chooseEvent[1])
            {
                case 1:
                    luckyEvent1();
                    break;
                case 2:
                    luckyEvent2();
                    break;
                case 3:
                    luckyEvent3();
                    break;
                case 4:
                    luckyEvent4();
                    break;
                case 5:
                    luckyEvent5();
                    break;
            }
        }
        else if(chooseEvent[0] == 2)
        {
            switch (chooseEvent[1])
            {
                case 1:
                    unluckyEvent1();
                    break;
                case 2:
                    unluckyEvent2();
                    break;
                case 3:
                    unluckyEvent3();
                    break;
                case 4:
                    unluckyEvent4();
                    break;
            }
        }
        else
        {
            normalEvent();
        }

        //increase mining stat after event plays
        player.mining++;

        // functions for all the events
        void normalEvent()
        {
            Debug.Log("normal");
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(1, 4);
                    player.stone += gStone;
                    messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone";
                    if(gCopper != 0)
                    {
                        messageBoxText.text += " and " + gCopper.ToString() + " Copper!";
                    }
                    else
                    {
                        messageBoxText.text += "!";
                    }
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    if (gIron != 0)
                    {
                        messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    else
                    {
                        messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    }
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    if(gGold != 0)
                    {
                        messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    else
                    {
                        messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    if (gTitanium != 0)
                    {
                        messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    }
                    else
                    {
                        messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(2, 5);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = normalMessage + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
            }
        }

        void luckyEvent1()
        {
            Debug.Log("lucky1");
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(1, 4);
                    player.stone += gStone;
                    player.mining++;
                    messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone!" + " Your Mining further improved - +2 Mining!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.mining++;
                    messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone";
                    if (gCopper != 0)
                    {
                        messageBoxText.text += " and " + gCopper.ToString() + " Copper!" + " Your Mining further improved - +2 Mining!";
                    }
                    else
                    {
                        messageBoxText.text += "!" + " Your Mining further improved - +2 Mining!";
                    }
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.mining++;
                    messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!" + " Your Mining further improved - +2 Mining!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.mining++;
                    if (gIron != 0)
                    {
                        messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!" + 
                            " Your Mining further improved - +2 Mining!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!" + " Your Mining further improved - +2 Mining!";
                    }
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.mining++;
                    if (gGold != 0)
                    {
                        messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!" + " Your Mining further improved - +2 Mining!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!" + 
                            " Your Mining further improved - +2 Mining!";
                    }
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.mining++;
                    messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!" + " Your Mining further improved - +2 Mining!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.mining++;
                    if (gTitanium != 0)
                    {
                        messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!" + " Your Mining further improved - +2 Mining!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!" + " Your Mining further improved - +2 Mining!";
                    }
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.mining++;
                    messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!" + " Your Mining further improved - +2 Mining!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(2, 5);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.mining++;
                    messageBoxText.text = luckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!" + " Your Mining further improved - +2 Mining!";
                    break;
            }
        }

        void luckyEvent2()
        {
            Debug.Log("lucky2");
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(1, 4);
                    player.stone += gStone;
                    player.strength++;
                    messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone!" + " Your Strength further improved - +1 Strength!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.strength++;
                    messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone";
                    if (gCopper != 0)
                    {
                        messageBoxText.text += " and " + gCopper.ToString() + " Copper!" + " Your Strength further improved - +1 Strength!";
                    }
                    else
                    {
                        messageBoxText.text += "!" + " Your Strength further improved - +1 Strength!";
                    }
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.strength++;
                    messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!" + " Your Strength further improved - +1 Strength!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.strength++;
                    if (gIron != 0)
                    {
                        messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!"
                            + " Your Strength further improved - +1 Strength!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!" + " Your Strength further improved - +1 Strength!";
                    }
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.strength++;
                    if (gGold != 0)
                    {
                        messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!" + " Your Strength further improved - +1 Strength!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!" + 
                            " Your Strength further improved - +1 Strength!";
                    }
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.strength++;
                    messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!" + " Your Strength further improved - +1 Strength!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.strength++;
                    if (gTitanium != 0)
                    {
                        messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!" + " Your Strength further improved - +1 Strength!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!" + " Your Strength further improved - +1 Strength!";
                    }
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.strength++;
                    messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!" + " Your Strength further improved - +1 Strength!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(2, 5);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.strength++;
                    messageBoxText.text = luckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!" + " Your Strength further improved - +1 Strength!";
                    break;
            }
        }

        void luckyEvent3()
        {
            Debug.Log("lucky3");
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(4, 7);
                    player.stone += gStone;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(3, 6);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone" + " and " + gCopper.ToString() + " Copper!";
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(4, 7);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(3, 6);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(3, 6);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                        gGold.ToString() + " Gold!";
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(4, 7);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(3, 6);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                        gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(4, 7);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(5, 8);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = luckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
            }
        }

        void luckyEvent4()
        {
            Debug.Log("lucky4");
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(1, 4);
                    player.stone += gStone;
                    player.stamina += 18;
                    messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.stamina += 18;
                    messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone";
                    if (gCopper != 0)
                    {
                        messageBoxText.text += " and " + gCopper.ToString() + " Copper!";
                    }
                    else
                    {
                        messageBoxText.text += "!";
                    }
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.stamina += 18;
                    messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.stamina += 18;
                    if (gIron != 0)
                    {
                        messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    }
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.stamina += 18;
                    if (gGold != 0)
                    {
                        messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.stamina += 18;
                    messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.stamina += 18;
                    if (gTitanium != 0)
                    {
                        messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.stamina += 18;
                    messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(2, 5);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    player.stamina += 18;
                    messageBoxText.text = luckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
            }
        }

        void luckyEvent5()
        {
            // IMPLEMENT SKIN REWARD LATER
            Debug.Log("lucky5");
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(1, 4);
                    player.stone += gStone;
                    messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone";
                    if (gCopper != 0)
                    {
                        messageBoxText.text += " and " + gCopper.ToString() + " Copper!";
                    }
                    else
                    {
                        messageBoxText.text += "!";
                    }
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    if (gIron != 0)
                    {
                        messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    }
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    if (gGold != 0)
                    {
                        messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    if (gTitanium != 0)
                    {
                        messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    }
                    else
                    {
                        messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(2, 5);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = luckyMessage5 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
            }
        }

        void unluckyEvent1()
        {
            Debug.Log("unlucky1");
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(0, 3);
                    player.stone += gStone;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone!";
                    break;
                case 2:
                    gStone = Random.Range(1, 4);
                    gCopper = Random.Range(0, 2);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 3:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 4:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(1, 4);
                    gIron = Random.Range(0, 2);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    break;
                case 5:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 3);
                    gGold = Random.Range(0, 2);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
    gGold.ToString() + " Gold!";
                    break;
                case 6:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    break;
                case 7:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(1, 4);
                    gTitanium = Random.Range(0, 2);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
    gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 8:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 9:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage1 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
            }
        }

        void unluckyEvent2()
        {
            Debug.Log("unlucky2");
            player.stamina -= 18;
            if(player.stamina < 0)
            {
                player.stamina = 0;
            }

            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(1, 4);
                    player.stone += gStone;
                    messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone";
                    if (gCopper != 0)
                    {
                        messageBoxText.text += " and " + gCopper.ToString() + " Copper!";
                    }
                    else
                    {
                        messageBoxText.text += "!";
                    }
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    if (gIron != 0)
                    {
                        messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    else
                    {
                        messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    }
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    if (gGold != 0)
                    {
                        messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    else
                    {
                        messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    if (gTitanium != 0)
                    {
                        messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    }
                    else
                    {
                        messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(2, 5);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage2 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
            }
        }

        void unluckyEvent3()
        {
            Debug.Log("unlucky3");
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(0, 2);
                    player.stone += gStone;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 1);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(0, 2);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 1);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 1);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                        gGold.ToString() + " Gold!";
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(0, 2);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 1);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                        gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(0, 2);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage3 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
            }
        }

        void unluckyEvent4()
        {
            Debug.Log("unlucky4");
            player.mining--;
            switch (miningTier)
            {
                case 1:
                    gStone = Random.Range(1, 4);
                    player.stone += gStone;
                    messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone!";
                    break;
                case 2:
                    gStone = Random.Range(2, 5);
                    gCopper = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone";
                    if (gCopper != 0)
                    {
                        messageBoxText.text += " and " + gCopper.ToString() + " Copper!";
                    }
                    else
                    {
                        messageBoxText.text += "!";
                    }
                    break;
                case 3:
                    gStone = Random.Range(3, 6);
                    gCopper = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    break;
                case 4:
                    gStone = Random.Range(4, 7);
                    gCopper = Random.Range(2, 5);
                    gIron = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    if (gIron != 0)
                    {
                        messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    else
                    {
                        messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone and " + gCopper.ToString() + " Copper!";
                    }
                    break;
                case 5:
                    gStone = Random.Range(5, 8);
                    gCopper = Random.Range(3, 6);
                    gIron = Random.Range(1, 4);
                    gGold = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    if (gGold != 0)
                    {
                        messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    else
                    {
                        messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, and " + gIron.ToString() + " Iron!";
                    }
                    break;
                case 6:
                    gStone = Random.Range(6, 9);
                    gCopper = Random.Range(4, 7);
                    gIron = Random.Range(2, 5);
                    gGold = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    break;
                case 7:
                    gStone = Random.Range(7, 10);
                    gCopper = Random.Range(5, 8);
                    gIron = Random.Range(3, 6);
                    gGold = Random.Range(2, 5);
                    gTitanium = Random.Range(0, 3);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    if (gTitanium != 0)
                    {
                        messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    }
                    else
                    {
                        messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, and " +
                            gGold.ToString() + " Gold!";
                    }
                    break;
                case 8:
                    gStone = Random.Range(8, 11);
                    gCopper = Random.Range(6, 9);
                    gIron = Random.Range(4, 7);
                    gGold = Random.Range(3, 6);
                    gTitanium = Random.Range(1, 4);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
                case 9:
                    gStone = Random.Range(9, 12);
                    gCopper = Random.Range(7, 10);
                    gIron = Random.Range(5, 8);
                    gGold = Random.Range(4, 7);
                    gTitanium = Random.Range(2, 5);
                    player.stone += gStone;
                    player.copper += gCopper;
                    player.iron += gIron;
                    player.gold += gGold;
                    player.titanium += gTitanium;
                    messageBoxText.text = unluckyMessage4 + " You got " + gStone.ToString() + " Stone, " + gCopper.ToString() + " Copper, " + gIron.ToString() + " Iron, " +
                            gGold.ToString() + " Gold, and " + gTitanium.ToString() + " Titanium!";
                    break;
            }
        }
    }

}
