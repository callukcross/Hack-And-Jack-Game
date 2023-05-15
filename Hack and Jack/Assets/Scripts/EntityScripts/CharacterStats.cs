using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //player stats
    public int strength;
    public int stamina;
    public int maxStamina = 100;
    public int mining;
    public int crafting;
    public int stone;
    public int copper;
    public int iron;
    public int gold;
    public int titanium;
    public int chesthair;
    public AxeManager equippedAxe;


    //player stat textboxes
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI miningText;
    public TextMeshProUGUI craftingText;
    public TextMeshProUGUI chesthairText;
    public TextMeshProUGUI axeText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI copperText;
    public TextMeshProUGUI ironText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI titaniumText;

    void Start()
    {
        //note: axe text is initialized in the start function of the axemanager script
        strengthText.text = "Strength: " + strength.ToString();
        staminaText.text = "Stamina: " + stamina.ToString() + "%";
        miningText.text = "Mining: " + mining.ToString();
        craftingText.text = "Crafting: " + crafting.ToString();
        stoneText.text = "Stone: " + stone.ToString();
        copperText.text = "Copper: " + copper.ToString();
        ironText.text = "Iron: " + iron.ToString();
        goldText.text = "Gold: " + gold.ToString();
        titaniumText.text = "Titanium: " + titanium.ToString();
        chesthairText.text = "Chesthair: Bare";
    }

    public void updateCharacterStats()
    {
        strengthText.text = "Strength: " + strength.ToString();
        staminaText.text = "Stamina: " + stamina.ToString() + "%";
        miningText.text = "Mining: " + mining.ToString();
        craftingText.text = "Crafting: " + crafting.ToString();
        stoneText.text = "Stone: " + stone.ToString();
        copperText.text = "Copper: " + copper.ToString();
        ironText.text = "Iron: " + iron.ToString();
        goldText.text = "Gold: " + gold.ToString();
        titaniumText.text = "Titanium: " + titanium.ToString();



        if (chesthair <= 7)
        {
            chesthairText.text = "Chesthair: Bare";
        }
        else if (chesthair > 7 && chesthair <= 14)
        {
            chesthairText.text = "Chesthair: Peach Fuzz";
        }
        else if (chesthair > 14 && chesthair <= 21)
        {
            chesthairText.text = "Chesthair: Hairy";
        }
        else if (chesthair > 21 && chesthair <= 29)
        {
            chesthairText.text = "Chesthair: Manly";
        }
        else
        {
            chesthairText.text = "Chesthair: Gorilla";
        }
    }
}
