using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("-- Position Settings --")] 
    [SerializeField] private float minYPos = -100;
    
    [Header("-- Movement Settings --")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float movementForce;
    [SerializeField] private float maxGroundSpeed;
    
    [Header("-- Camera Settings --")]
    [SerializeField, Tooltip("The camera that rotates around the player")] private GameObject cameraGameObject;
    [SerializeField, Tooltip("The maximum angle the camera can look up or down.")] private float maxVerticalCameraAngle = 85;
    [SerializeField, Tooltip("What should be the camera's position in relation to the player.")] private Vector3 cameraOffset = Vector3.back * 10;
    [SerializeField, Tooltip("what position in relation to the player should the camera be looking at.")] private Vector3 cameraLookPosition = Vector3.up;

    [Header("-- Animation Settings --")]
    [SerializeField] private Animator animator;
    private Vector3 currentVelocity;
    private Vector3 controlForce;
    private bool hasJumped = false;
    //The current x and y rotation of the camera
    private float currentCameraXRotation = 0;
    private float currentCameraYRotation = 0;
    private Quaternion targetRotation = Quaternion.identity;

    private Vector3 initialPosition;
    
    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialPosition = transform.position;
    }

    private void UpdateVelocity()
    {
        hasJumped = false;
        currentVelocity = characterController.velocity + Physics.gravity * Time.fixedDeltaTime;
        currentVelocity += controlForce;
        if(characterController.isGrounded)
        {
            currentVelocity -= characterController.velocity * (characterController.velocity.magnitude / maxGroundSpeed);
        }
    }
    
    private void FixedUpdate()
    {
        if(transform.position.y < minYPos)
        {
            transform.position = initialPosition;
            
            currentVelocity = Vector3.zero;
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            UpdateVelocity();
            characterController.SimpleMove(currentVelocity);
            hasJumped = false;
        }
    }

    private void Update()
    {
        if(characterController.isGrounded)
        {
            animator.SetBool("Airborne", false);
            bool isRunning = false;
            targetRotation = Quaternion.Euler(0, cameraGameObject.transform.rotation.eulerAngles.y,0);
            controlForce = targetRotation * new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized * (movementForce * Time.fixedDeltaTime);
            if(Input.GetAxisRaw("Fire3") > 0.1f)
            {
                controlForce *= 2;
                isRunning = true;
            }

            if(controlForce.magnitude > 0.1f)
            {
                if(isRunning)
                {
                    animator.SetInteger("MoveSpeed", 2);
                }
                else
                {
                    animator.SetInteger("MoveSpeed", 1);
                }
            }
            else
            {
                animator.SetInteger("MoveSpeed", 0);
            }
            
            if(Input.GetAxisRaw("Jump") > 0.1f || hasJumped)
            {
                controlForce = controlForce + Vector3.up * jumpSpeed;
                hasJumped = true;
            }
        }
        else
        {
            animator.SetBool("Airborne", true);
        }
        if(cameraGameObject)
        {
            currentCameraXRotation += Input.GetAxisRaw("Mouse Y");
            currentCameraYRotation += Input.GetAxisRaw("Mouse X");
            //clamp the camera rotation to be less than the max and greater than the min
            currentCameraXRotation = Mathf.Clamp(currentCameraXRotation, -maxVerticalCameraAngle, maxVerticalCameraAngle);
            //set the position and rotation of the camera according to the current camera rotation variables.
            cameraGameObject.transform.position = gameObject.transform.position + Quaternion.Euler(-currentCameraXRotation, currentCameraYRotation, 0) * cameraOffset;
            cameraGameObject.transform.LookAt(transform.position + cameraLookPosition);
        }
    }
}
