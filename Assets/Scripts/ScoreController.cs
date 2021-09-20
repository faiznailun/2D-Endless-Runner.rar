using System.Collections;
using System.Collections.Generic;
using UnityEngine;

private int currentScore = 0;

private void Start()
{
    // reset
    currentScore = 0;
}

public float GetCurrentScore()
{
    return currentScore;
}

public void IncreaseCurrentScore(int increment)
{
    currentScore += increment;
}

public void FinishScoring()
{
    // set high score
    if (currentScore > ScoreData.highScore)
    {
        ScoreData.highScore = currentScore;
    }
}

[Header("Scoring")]
public ScoreController score;
public float scoringRatio;
private float lastPositionX;

private void Update()
{
    // read input
    if (Input.GetMouseButtonDown(0))
    {
        if (isOnGround)
        {
            isJumping = true;

            sound.PlayJump();
        }
    }

    // change animation
    anim.SetBool("isOnGround", isOnGround);

    // calculate score
    int distancePassed = Mathf.FloorToInt(transform.position.x - lastPositionX);
    int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);

    if (scoreIncrement > 0)
    {
        score.IncreaseCurrentScore(scoreIncrement);
        lastPositionX += distancePassed;
    }
}