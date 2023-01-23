using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Exercise : MonoBehaviour
{
    public CharacterStats player; //references the player stats
    public NonCharacterStats daytree; // references the nonplayer stats
    public TextMeshProUGUI messageBoxText; // references the message box

    private int[] chesthairModifier = { 0, 0 }; //when rolling RNG for lucky and unlucky events, this value is determined by the player chesthair stat.
    private int[] chooseEvent = { 0, 0 }; // [0] 0 = normal, 1 = lucky, 2 = unlucky    [1] specific event numbers

    //All messages for the events
    // normal event is +1 strength
    // lucky1 is +2 strength
    // lucky2 is +1 strength and only -4 stamina instead of normal -16 stamina to perform action.
    // unlucky event is +1 strength but double stamina loss for the day
    private string normalMessage = "You find the nearest boulder and start lifting it. After some quality time with your rock, you feel stronger! +1 Strength!"; //choseEvent[0][0]
    private string luckyMessage1 = "Your muscles start to burst as you climb a cliffside with a tree tied to your back, roots and all still attached! +2 Strength!";//chooseEvent[1][1]
    private string luckyMessage2 = "You wake up to birds chirping, sunny weather and a gentle breeze. You feel raring and ready to go! You spent only 4 Stamina today! +1 Strength!";//chooseEvent[1][2]
    private string unluckyMessage = "You hear a snap as you benchpress a moose. Your body aches terribly, leading you to end the day early. Double Stamina loss for today. +1 Strength!";//chooseEvent[2][0]

    public void playExerciseEvent()
    {
        //check if player has necessary stamina to perform action - if yes, remove 16 stamina, if no, kill action
        if (player.stamina < 16)
        {
            messageBoxText.text = "Not enough Stamina to exercise today! You should consider resting.";
            return;
        }
        else
        {
            player.stamina -= 16;
            if(player.stamina < 0)
            {
                player.stamina = 0;
            }
        }

        //increment day count
        daytree.day++;

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
        if (randnum <= chesthairModifier[1]) //unlucky event check
        {
            chooseEvent[0] = 2; //unlucky event
            chooseEvent[1] = 0;
        }
        else if (randnum >= (100 - chesthairModifier[0])) //lucky event check
        {
            chooseEvent[0] = 1; //lucky event
        }
        else //normal event check
        {
            chooseEvent[0] = 0; //normal event
            chooseEvent[1] = 0;
        }

        //rng to determine which lucky event plays if the chosen type is lucky - not done with unlucky considering there is only one unlucky event
        if(chooseEvent[0] == 1)
        {
            chooseEvent[1] = Random.Range(1, 3);
        }
        else
        {
            chooseEvent[1] = 1;
        }

        //call the event that was chosen by RNG above
        if(chooseEvent[0] == 0)
        {
            normalEvent();
        }
        else if(chooseEvent[0] == 1)
        {
            switch(chooseEvent[1])
            {
                case 1:
                    luckyEvent1();
                    break;
                case 2:
                    luckyEvent2();
                    break;
            }
        }
        else if(chooseEvent[0] == 2)
        {
            unluckyEvent();
        }

        //increase strength stat after event plays
        player.strength++;

        void normalEvent()
        {
            messageBoxText.text = normalMessage;
        }

        void luckyEvent1()
        {
            player.strength++;
            messageBoxText.text = luckyMessage1;
        }

        void luckyEvent2()
        {
            player.stamina += 12;
            messageBoxText.text = luckyMessage2;
        }

        void unluckyEvent()
        {
            player.stamina -= 16;
            if(player.stamina < 0)
            {
                player.stamina = 0;
            }
            messageBoxText.text = unluckyMessage;
        }
    }
}
