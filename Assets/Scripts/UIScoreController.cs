using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Header("UI")]
public Text score;
public Text highScore;

[Header("Score")]
public ScoreController scoreController;

private void Update()
{
    score.text = scoreController.GetCurrentScore().ToString();
    highScore.text = ScoreData.highScore.ToString();
}