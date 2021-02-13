using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreContainer : MonoBehaviour
{

    public static ScoreContainer scoreScript;

    [SerializeField]
    Text scoreText = null;
    [SerializeField]
    Text timeText = null;

    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform mouse = null;

    public int score;
    public float time;
    int waitToScore;

    private void Start()
    {
        scoreScript = this;
        time = 0;
        score = 0;
        waitToScore = 0;
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (Vector2.Distance(player.position, mouse.position) < 0.00001f)  // if player is not moving
        {
            if (waitToScore < 0)
            {
                waitToScore = 0;
            }

            if (waitToScore == 5)
            {
                waitToScore = 0;
                score++; //gain points
            }
            waitToScore++;
        }
        else    // if player is moving
        {
            if (waitToScore > 0) { 
                waitToScore = 0;
            }

            if (waitToScore == -6)
            {
                waitToScore = 0;
                if (score > 0)
                {
                    score--; //lose points
                }
            }
            waitToScore--;
        }


        timeText.text = "Time: " + FormatTime(time);
        scoreText.text = "Score: " + score;
    }

    public string FormatTime(float time)
    {
        int d = (int)(time * 100.0f);
        int minutes = d / (60 * 100);
        int seconds = (d % (60 * 100)) / 100;
        int hundredths = d % 100;
        if (minutes > 0) 
        {        
            return String.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
        }
        if (seconds > 0)
        {
            return String.Format("{0:00}.{1:00}", seconds, hundredths);
        }

        return String.Format("{0:00}", hundredths);
    }

}
