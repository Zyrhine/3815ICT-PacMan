using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private TMP_Text scoreText;

    void Start()
    {
        scoreText = GameObject.Find("view/Canvas/ScoreText").GetComponent<TMP_Text>();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateLives(int lives)
    {
        switch (lives)
        {
            case 0:
                GameObject.Find("view/Canvas/Life1").SetActive(false);
                break;
            case 1:
                GameObject.Find("view/Canvas/Life2").SetActive(false);
                break;
            case 2:
                GameObject.Find("view/Canvas/Life3").SetActive(false);
                break;
        }
    }

    public void ShowWinPanel()
    {
        GameObject.Find("view/Canvas/WinPanel").SetActive(true);
    }

    public void ShowLosePanel()
    {
        GameObject.Find("view/Canvas/LosePanel").SetActive(true);
    }
}
