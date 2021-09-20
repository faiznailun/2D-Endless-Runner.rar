using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    public AudioClip jump;

    public void PlayJump()
    {
        audioPlayer.PlayOneShot(jump);
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
    }

    private AudioSource audioPlayer;

    private CharacterSoundController sound;

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
    }



}


public AudioClip scoreHighlight;

public void PlayScoreHighlight()
{
    audioPlayer.PlayOneShot(scoreHighlight);
}

[Header("Score Highlight")]
public int scoreHighlightRange;
public CharacterSoundController sound;

private int lastScoreHighlight = 0;

private void Start()
{
    // reset
    currentScore = 0;
    lastScoreHighlight = 0;
}

public void IncreaseCurrentScore(int increment)
{
    currentScore += increment;

    if (currentScore - lastScoreHighlight > scoreHighlightRange)
    {
        sound.PlayScoreHighlight();
        lastScoreHighlight += scoreHighlightRange;
    }
}