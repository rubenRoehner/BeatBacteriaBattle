using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI levelLabel;
    public int TimerStartValue = 150;
    private float timeRemaining = 0;


    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0f;
        timeRemaining = TimerStartValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0 )
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        } else
        {
            DisplayTime(0);
            GameManager.Instance.GameOver();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateLevelLabel(int level)
    {
        levelLabel.text = "Level " + level;
    }
}
