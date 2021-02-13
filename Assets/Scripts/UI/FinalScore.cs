using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{

    [SerializeField]
    Text scoreText = null;
    [SerializeField]
    Text timeText = null;

    private void Update()
    {
        scoreText.text ="Score: " + ScoreContainer.scoreScript.score;
        timeText.text = "Time: " + ScoreContainer.scoreScript.FormatTime(ScoreContainer.scoreScript.time);
    }
}
