using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    public float maximumSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float jumpSpeed;
    
    [SerializeField]
    private float jumpButtonGracePeriod;
    
    [SerializeField]
    private Transform cameraTransform;

    private CharacterController characterController;
    private PlayerInput playerInput;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    [HideInInspector] 
    public bool canMove;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        canMove = true;
    }
        
    void Update()
    {
        Movement();
        Jump();
    }

    private void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;
    
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        
        if (playerInput.jump)
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
    
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }
    }
    
    private void Movement()
    {
        if (canMove)
        {
            Vector3 movementDirection = new Vector3(playerInput.horizontalInput, 0, playerInput.verticalInput);
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
            
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                inputMagnitude /= 2;
                //transform.localScale = new Vector3(transform.localScale.x + sizeSpeed, transform.localScale.y + sizeSpeed, transform.localScale.z + sizeSpeed);
            }

            float speed = inputMagnitude * maximumSpeed;
            movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
            movementDirection.Normalize();

            Vector3 velocity = movementDirection * speed;
            velocity.y = ySpeed;
    
            characterController.Move(velocity * Time.deltaTime);
    
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
    
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
    
    private void OnApplicationFocus(bool focus)
    {
        //Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
