using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 8f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public Camera playerCamera;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Transform myTransform = transform;
        Vector3 move = myTransform.right * x + myTransform.forward * z; // This makes it so its moving locally so rotation is taken into consideration

        controller.Move(move * (playerSpeed * Time.deltaTime)); // Moving in the direction of move at the speed

        // Changes the height position of the player..
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
