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

    // Start is called before the first frame update
    void Start()
    {
        scoreUI.text = "Score: 0";
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTimer(float time)
    {
        timerUI.text = "Timer: " + Mathf.RoundToInt(time).ToString();
    }

    public void setScore(int score)
    {
        scoreUI.text = "Score: " + score.ToString();
    }

    public void setGameOver(bool won)
    {
        gameOverPanel.gameObject.SetActive(true);
        if (won)
        {
            Cursor.lockState = CursorLockMode.None;
            gameOverText.text = "You won!";
        } else
        {
            Cursor.lockState = CursorLockMode.None;
            gameOverText.text = "You lose";
        }
    }

    public void fillCastingBar(float percent)
    {

        percent = Mathf.Round(percent * 100f) / 100f;
        castingProgress.fillAmount = percent;
        fillPercentage.text = (percent * 100).ToString() + "%";
    }
}
