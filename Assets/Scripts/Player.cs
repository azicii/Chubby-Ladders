using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float playerHeight = 2f;

    [Header("Keybinds")]
    [SerializeField] KeyCode forwardJumpKey = KeyCode.Space;
    [SerializeField] KeyCode backwardJumpKey = KeyCode.LeftControl;

    Transform playerTransform;

    bool isGrounded;

    float forwardJumpDistance = 4.34f;
    float verticalJumpDistance = 2.17f;

    void Start()
    {
        playerTransform = GetComponentInChildren<PlayerBody>().gameObject.transform;
    }

    void Update()
    {
        //Sends a ray below player to check it platform collider is below. Returns true if collider is present.
        isGrounded = Physics.Raycast(playerTransform.position, Vector3.down, (playerHeight));
        //Debug.Log(isGrounded);

        HandleJumps();

    }

    void HandleJumps()
    {
        if (Input.GetKeyDown(forwardJumpKey) && isGrounded)
        {
            ForwardJump();
        }

        if (Input.GetKeyDown(backwardJumpKey) && isGrounded)
        {
            BackWardJump();
        }
    }

    void ForwardJump()
    {
        Debug.Log("Foward Jump!");
        playerTransform.position += Vector3.right * (4.34f) + Vector3.up * verticalJumpDistance;
    }

    void BackWardJump()
    {
        Debug.Log("Backward Jump!");
        playerTransform.Rotate(0, 180, 0);

        playerTransform.position += -Vector3.right * forwardJumpDistance + Vector3.up * verticalJumpDistance;
    }
}
