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

    Transform playerTransform;
    PlayerUI playerUI;
    Camera mainCamera;
    Rigidbody rb;
    

    public Platforms platforms;

    public bool isGrounded;
    bool isFacingRight;
    public bool gameOver = false;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI instructions;

    float forwardJumpDistance = 4.34f;
    float verticalJumpDistance = 2.17f;

    void Start()
    {
        playerTransform = GetComponentInChildren<PlayerBody>().gameObject.transform;
        playerUI = GetComponent<PlayerUI>();
        mainCamera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();

        isFacingRight = true;
    }

    void Update()
    {
        //Sends a ray below player to check it platform collider is below. Returns true if collider is present.
        isGrounded = Physics.Raycast(playerTransform.position, Vector3.down, (playerHeight));
        Debug.Log(isGrounded);

        HandleJumps();
        CheckGameOver();
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

        //Follow the player with camera
        mainCamera.transform.position += Vector3.up * verticalJumpDistance;
        mainCamera.transform.position += vec * forwardJumpDistance;

        //Add to score
        playerUI.AddPoint();

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

        //Follow the player with camera
        mainCamera.transform.position += Vector3.up * verticalJumpDistance;
        mainCamera.transform.position += vec * forwardJumpDistance;

        //Add to score
        playerUI.AddPoint();

        //Remove instructions
        playerUI.RemoveInstructions();
    }

    void CheckGameOver()
    {
        if (!isGrounded)
        {
            rb.isKinematic = false;
            gameOver = true;
            
            playerUI.ShowGameOverScreen();
        }
    }
}
