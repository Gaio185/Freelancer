using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    public bool hasClearance;
    public bool isHunted;
    public bool canExtract;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    Quaternion initialRotation;
    public float amt, slerpAmt;
    private float targetZ;

    public bool canMove = true;

    // Variables for door interaction
    public float interactionRange = 10f;  // Raycast range for door interaction

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isHunted = false;
        hasClearance = true;
        canExtract = false;

        initialRotation = transform.localRotation;  // Save the initial rotation
        targetZ = initialRotation.eulerAngles.z;    // Set initial target z-axis rotation
    }

    void Update()
    {
        // Player movement logic
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftControl) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Looking around with mouse
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Check for player leaning
        if(canMove) LeanCheck();

        //// Try interacting with door when the player presses "F"
        //if (Input.GetKeyDown(KeyCode.F) && canMove)
        //{
        //    TryInteractWithDoor();
        //}
    }

    // Raycast to find doors and interact with them
    //void TryInteractWithDoor()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange))
    //    {
    //        Door targetDoor = hit.transform.GetComponent<Door>();
    //        if (targetDoor != null)
    //        {
    //            targetDoor.TryOpenDoor();  // Call the method to open the door if unlocked
    //        }
    //        else
    //        {
    //            Debug.Log("No door in front of you.");
    //        }
    //    }
    //}

    void LeanCheck()
    {
        // Get the current Euler angles (yaw, pitch, and roll)
        Vector3 currentEulerAngles = transform.localEulerAngles;

        // Determine target z-axis based on input
        if (Input.GetKey(KeyCode.Q))
        {
            // Lean left: increase the z-axis rotation gradually
            targetZ = initialRotation.eulerAngles.z + amt;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            // Lean right: decrease the z-axis rotation gradually
            targetZ = initialRotation.eulerAngles.z - amt;
        }
        else
        {
            // No input: smoothly return to the initial z-axis rotation
            targetZ = initialRotation.eulerAngles.z;
        }

        // Smoothly interpolate the z-axis rotation
        currentEulerAngles.z = Mathf.LerpAngle(currentEulerAngles.z, targetZ, Time.deltaTime * slerpAmt);

        // Apply the updated rotation back to the transform
        transform.localRotation = Quaternion.Euler(currentEulerAngles);
    }
}
