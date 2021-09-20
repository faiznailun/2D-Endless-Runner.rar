using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;

    [Header("Jump")]
    public float jumpAccel;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    private bool isJumping;
    private bool isOnGround;

    private Rigidbody2D rig;
    private Animator anim;


    // Start is called before the first frame update
     private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isOnGround)
            {
                isJumping = true;
            }
        }

        // change animation
        anim.SetBool("isOnGround", isOnGround);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        Vector2 velocityVector = rig.velocity;

        if (isJumping)
        {
            velocityVector.y += jumpAccel;
        }

        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        rig.velocity = velocityVector;

        // raycast ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);

        if (hit)
        {
            if (!isOnGround && rig.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else
        {
            isOnGround = false;
        }

    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundRaycastDistance), Color.white);
    }
}


[Header("GameOver")]
public GameObject gameOverScreen;
public float fallPositionY;

[Header("Camera")]
public CameraMoveController gameCamera;


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

    // game over
    if (transform.position.y < fallPositionY)
    {
        GameOver();
    }
}

private void GameOver()
{
    // set high score
    score.FinishScoring();

    // stop camera movement
    gameCamera.enabled = false;

    // show gameover
    gameOverScreen.SetActive(true);

    // disable this too
    this.enabled = false;
}