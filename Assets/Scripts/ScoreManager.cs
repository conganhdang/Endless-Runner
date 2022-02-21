using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public Text diamondText;

    public Slider slider;

    public PickupEnergy gainGauge;

    public int maxGauge;
    public int currentGauge;
    public bool isFullGauge;
    private int initialGauge = 0;
    public float scoreCount;
    public float highScoreCount;
    private int diamondCount;
    private int diamondAmount;

    public float pointPerSecond;

    public bool scoreIncreasing;
    public bool diamonadIncreasing;
    // Start is called before the first frame update
    void Start()
    {
        isFullGauge = false;
        SetGauge(currentGauge);
        SetMaxGauge(maxGauge);
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
        // if(PlayerPrefs.HasKey("Diamond"))
        // {
        //     diamondCount = PlayerPrefs.GetInt("Diamond");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreIncreasing)
        {
            scoreCount += pointPerSecond * Time.deltaTime;
        }
        
        if(scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }
        if(diamonadIncreasing)
        {
            diamondAmount = diamondAmount + diamondCount;
            PlayerPrefs.SetInt("Diamond", diamondAmount);
        }
        
        scoreText.text = "Score: " + Mathf.Round (scoreCount) + "m";
        highScoreText.text = "High Score: " + Mathf.Round (highScoreCount);
        diamondText.text = "" + diamondCount;
    }

    public void AddScore(int pointToAdd)
    {
        diamondCount += pointToAdd;
    }

    public void AddGauge(int gaugeToAdd)
    {
        if (currentGauge < maxGauge)
        {
            currentGauge += gaugeToAdd;
        }
        SetGauge(currentGauge);
    }

    public void SetMaxGauge(int gauge)
    {
        slider.maxValue = gauge;
        slider.value = gauge;
    }

    public void SetGauge(int gauge)
    {
        slider.value = gauge;
    }

    
    public void ResetStat()
    {
        diamondCount = 0;
        currentGauge = 0;
        SetGauge(0);
    }
}
