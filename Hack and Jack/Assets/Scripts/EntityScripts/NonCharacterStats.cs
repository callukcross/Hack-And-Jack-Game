using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonCharacterStats : MonoBehaviour
{
    public int day;
    public float currentTreeHP;
    public float maxTreeHP;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI treeHPText;

    void Start()
    {
        day = 1;
        currentTreeHP = 4000;
        maxTreeHP = 4000;
        // later implement ability to change treeHP for different game lengths

        dayText.text = "Day: " + day.ToString();
        treeHPText.text = "Tree HP:  " + currentTreeHP.ToString() + "/" + maxTreeHP.ToString();
    }

    public void updateNonCharacterStats()
    {
        dayText.text = "Day: " + day.ToString();
        treeHPText.text = "Tree HP:  " + currentTreeHP.ToString() + "/" + maxTreeHP.ToString();
    }
}
