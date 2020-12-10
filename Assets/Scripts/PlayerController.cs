using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 3f;

    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    public float upperLimit = -50;
    public float lowerLimit = 50;

    private float verticalSpeed = 0;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move(){
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Checks whether the character is currently on the ground. If so, and the jump button is pressed,
        // the character will jump up and then fall down with gravity.
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && verticalSpeed < 0) {
            verticalSpeed = 0f;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            verticalSpeed += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Sprinting
        if (Input.GetKeyDown (KeyCode.LeftShift))
        {
            speed = 6f;
        } 
        if (Input.GetKeyUp (KeyCode.LeftShift)) {
            speed = 3f;
        }
        
        // Sneaking
        if (Input.GetKeyDown (KeyCode.LeftControl)) {
            speed = 1.5f;
        }
        if (Input.GetKeyUp (KeyCode.LeftControl)) {
            speed = 3f;
        }

        verticalSpeed += gravityValue * Time.deltaTime;

        Vector3 gravityMovement = new Vector3(0, verticalSpeed, 0);
        
        Vector3 movement = transform.forward * verticalMovement + transform.right * horizontalMovement;
        characterController.Move(movement * speed * Time.deltaTime + gravityMovement * Time.deltaTime);
    }

    void Rotate(){
        float horizRot = Input.GetAxis("Mouse X");
        float vertRot = Input.GetAxis("Mouse Y");

        // Rotates the player (the game object this script is attached to)
        transform.Rotate(0, horizRot * mouseSensitivity, 0);
        // Rotates the camera up and down
        cameraTransform.Rotate(-vertRot*mouseSensitivity, 0, 0);

        // Holds the rotation of the camera around the x, y, and z axes
        Vector3 currentRotation = cameraTransform.localEulerAngles;
        if (currentRotation.x > 180){
            currentRotation.x -= 360;
        }
        currentRotation.x = Mathf.Clamp(currentRotation.x, upperLimit, lowerLimit);
        cameraTransform.localRotation = Quaternion.Euler(currentRotation);
    }
}
