using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreen;

    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public int score;
    

    public void UpdateLives(int currentLife)
    {
        livesImageDisplay.sprite = lives[currentLife];
    }

    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "SCORE: " + score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "SCORE: ";
        score = 0;
    }
}
