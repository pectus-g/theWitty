using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NL_Score : MonoBehaviour
{
    public static int totalLootAmount;
    public static int pickedLoot;
    private string finalString;

    public Text scoreText;

    public Text finalScoreText;

    private void Awake()
    {
        pickedLoot = 0;
        totalLootAmount = 0;
    }

    public void UpdateScore(int addValue)
    {
        pickedLoot += addValue;
        finalString = pickedLoot.ToString() + "/" + totalLootAmount.ToString();
        scoreText.text = finalString;
    }

    public void UpdateFinalScore()
    {
        if (finalScoreText == null) return;

        finalScoreText.text = pickedLoot.ToString() + " of " + totalLootAmount.ToString() + " diamonds collected!";
    }
}
