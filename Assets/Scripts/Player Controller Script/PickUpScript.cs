using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float pickUpRange = 10f; // Increase range to make objects easier to pick up
    public float pickUpRadius = 1f; // Radius of the SphereCast for easier object pickup
    private float rotationSensitivity = 1f; // How fast/slow the object is rotated in relation to mouse movement
    private GameObject heldObj; // Object which we pick up
    private Rigidbody heldObjRb; // Rigidbody of object we pick up
    private bool canDrop = true; // This is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; // Layer index

    // Reference to script which includes mouse movement of player (looking around)
    // we want to disable the player looking around when rotating the object
    // example below 
    // MouseLookScript mouseLookScript;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); // Check if the layer exists

        if (LayerNumber == -1) // Invalid layer check
        {
            Debug.LogError("Layer 'holdLayer' does not exist! Please check your layer setup.");
        }

        // mouseLookScript = player.GetComponent<MouseLookScript>();
    }

    void Update()
    {
        // Check if the F key is pressed to pick up an object
        if (Input.GetKeyDown(KeyCode.F) && heldObj == null) // Only pick up if not holding anything
        {
            // Perform sphere cast for easier object pickup
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, pickUpRadius, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
                // Make sure pickup tag is attached
                if (hit.transform.gameObject.tag == "canPickUp")
                {
                    // Pass in object hit into the PickUpObject function
                    PickUpObject(hit.transform.gameObject);
                }
            }
        }

        // Check if G key is pressed to drop the object
        if (Input.GetKeyDown(KeyCode.G) && heldObj != null) // Only drop if holding something
        {
            if (canDrop)
            {
                StopClipping(); // Prevents object from clipping through walls
                DropObject();
            }
        }

        if (heldObj != null) // If player is holding an object
        {
            MoveObject(); // Keep object position at holdPos
            RotateObject(); // Allow object rotation
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) // Make sure the object has a Rigidbody
        {
            heldObj = pickUpObj; // Assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); // Assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; // Parent object to holdPosition

            if (LayerNumber != -1) // Only change layer if valid
            {
                heldObj.layer = LayerNumber; // Change the object layer to the holdLayer
            }
            else
            {
                Debug.LogWarning("Unable to assign 'holdLayer' to the object. Keeping the default layer.");
            }

            // Make sure object doesn't collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        // Re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; // Object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; // Unparent object
        heldObj = null; // Undefine game object
    }

    void MoveObject()
    {
        // Keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R)) // Hold R key to rotate, change this to whatever key you want
        {
            canDrop = false; // Make sure throwing can't occur during rotating

            // Disable player being able to look around
            // mouseLookScript.verticalSensitivity = 0f;
            // mouseLookScript.lateralSensitivity = 0f;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            // Rotate the object depending on mouse X-Y Axis
            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            // Re-enable player being able to look around
            // mouseLookScript.verticalSensitivity = originalvalue;
            // mouseLookScript.lateralSensitivity = originalvalue;
            canDrop = true;
        }
    }

    void StopClipping() // Function only called when dropping
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); // Distance from holdPos to the camera
        // Have to use RaycastAll as object blocks raycast in center screen
        // RaycastAll returns array of all colliders hit within the clipRange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        // If the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            // Change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); // Offset slightly downward to stop object dropping above player 
        }
    }
}
