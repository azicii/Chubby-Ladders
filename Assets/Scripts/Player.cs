using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    float playerHeight = 2f;

    [Header("Keybinds")]
    [SerializeField] KeyCode forwardJumpKey = KeyCode.Space;
    [SerializeField] KeyCode backwardJumpKey = KeyCode.LeftControl;
    [SerializeField] KeyCode restartKey = KeyCode.R;

    Transform playerTransform;
    PlayerUI playerUI;
    Rigidbody rb;
    

    public Platforms platforms;

    public bool isGrounded;
    bool isFacingRight;
    public bool gameOver = false;

    [SerializeField] TextMeshProUGUI scoreText;

    float forwardJumpDistance = 4.34f;
    float verticalJumpDistance = 2.17f;
    readonly float[] timerMaxSecs = { 3f, 2f, 1f };
    readonly int platformsPerLevel = 10;


    void Start()
    {
        playerTransform = GetComponentInChildren<PlayerBody>().gameObject.transform;
        playerUI = GetComponent<PlayerUI>();
        rb = GetComponentInChildren<Rigidbody>();

        isGrounded = true;

        isFacingRight = true;
    }

    void Update()
    {
        HandleJumps();

        HandleRespawn();
    }

    void HandleJumps()
    {
        if (Input.GetKeyDown(forwardJumpKey) && isGrounded)
        {
            ForwardJump();
        }

        if (Input.GetKeyDown(backwardJumpKey) && isGrounded)
        {
            BackwardJump();
        }
    }

    void ForwardJump()
    {
        Debug.Log("Foward Jump!");

        //Move up one platform
        playerTransform.position += Vector3.up * verticalJumpDistance;

        //Move same direction
        Vector3 vec = Vector3.left;
        if (isFacingRight)
        {
            vec = Vector3.right;
        }
        playerTransform.position += vec * forwardJumpDistance;

        //Add another platform
        platforms.AddNextPlatform();

        //Sends a ray below player to check it platform collider is below. Returns true if collider is present.
        isGrounded = Physics.Raycast(playerTransform.position, Vector3.down, (playerHeight));
        Debug.Log(isGrounded);

        CheckGameOver();

        if (gameOver) return;

        //Add to score
        playerUI.AddPoint();

        //Start timer countdown
        int timerInd = playerUI.rawScore / 10;
        if (timerInd >= timerMaxSecs.Length)
        {
            timerInd = timerMaxSecs.Length - 1;
        }
        playerUI.StartTimer(timerMaxSecs[timerInd]);

        //Remove instructions
        playerUI.RemoveInstructions();
    }

    void BackwardJump()
    {
        Debug.Log("Backward Jump!");

        //Turn player around
        playerTransform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;

        //Move up one platform
        playerTransform.position += Vector3.up * verticalJumpDistance;

        //Move opposite direction
        Vector3 vec = Vector3.left;
        if (isFacingRight)
        {
            vec = Vector3.right;
        }
        playerTransform.position += vec * forwardJumpDistance;

        //Add another platform
        platforms.AddNextPlatform();

        //Sends a ray below player to check it platform collider is below. Returns true if collider is present.
        isGrounded = Physics.Raycast(playerTransform.position, Vector3.down, (playerHeight));
        Debug.Log(isGrounded);

        CheckGameOver();

        if (gameOver) return;

        //Add to score
        playerUI.AddPoint();

        //Start timer countdown
        int timerInd = playerUI.rawScore / platformsPerLevel;
        if (timerInd >= timerMaxSecs.Length)
        {
            timerInd = timerMaxSecs.Length - 1;
        }
        playerUI.StartTimer(timerMaxSecs[timerInd]);

        //Remove instructions
        playerUI.RemoveInstructions();
    }

    void CheckGameOver()
    {
        if (!isGrounded)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            SetGameOver();
        }
    }

    void HandleRespawn()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(restartKey))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
        playerUI.ShowGameOverScreen();
    }
}
