using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public RectTransform gameOverPanel;
    public Text gameOverText;
    public Text timerUI;
    public Text scoreUI;
    public Text fillPercentage;
    public Image castingProgress;
    
    void Start()
    {        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
    }

    public void setTimer(float time)
    {
        timerUI.text = TextConstants.TIMER + Mathf.RoundToInt(time).ToString();
    }

    public void setScore(int score)
    {
        scoreUI.text = TextConstants.SCORE + score.ToString();
    }

    public void setGameOver(bool won)
    {
        gameOverPanel.gameObject.SetActive(true);
        if (won)
        {
            Cursor.lockState = CursorLockMode.None;
            gameOverText.text = TextConstants.VICTORY;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
            gameOverText.text = TextConstants.GAMEOVER;
        }
    }

    public void fillCastingBar(float percent)
    {

        percent = Mathf.Round(percent * 100f) / 100f;
        castingProgress.fillAmount = percent;
        fillPercentage.text = (percent * 100).ToString() + "%";
    }
}
