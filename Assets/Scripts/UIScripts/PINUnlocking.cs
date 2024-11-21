using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PINUnlocking : MonoBehaviour
{
    public TextMeshProUGUI ans; // Display for the entered PIN
    public TextMeshProUGUI placeHolder; // Placeholder for the PIN input
    public SafeDoorController safeDoorController;
    public UnlockSecretPassage unlockSecretPassage;
    private Player player;

    public string correctPIN; // Correct PIN

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void Number(int number)
    {
        // Clear placeholder text if it's visible
        if (placeHolder.text != "")
        {
            placeHolder.text = "";
        }

        // Append the number if the length is less than 4
        if (ans.text.Length < 4)
        {
            ans.text += number.ToString();
        }
        else
        {
            Debug.Log("PIN is 4 digits long");
        }
    }

    public void Delete()
    {
        // Remove the last character from the entered PIN
        if (ans.text.Length > 0)
        {
            ans.text = ans.text.Remove(ans.text.Length - 1);
        }
    }

    public void Clear()
    {
        ans.text = ""; // Clear the entered PIN
    }

    public void Enter()
    {
        // Check if the entered PIN is correct
        if (ans.text == correctPIN)
        {
            Debug.Log("Correct PIN entered");
            Clear();

            
            if (safeDoorController != null)
            {
                safeDoorController.UnlockSafe(); // Unlock the safe door
            }
            else if(unlockSecretPassage != null)
            {
                unlockSecretPassage.UnlockPassage(); // Unlock the secret passage
            }
     
        }
        else
        {
            Clear();
            placeHolder.text = "Incorrect PIN, try again"; 
        }
    }

    void Update()
    {
        // Capture keyboard input if the UI is active
        if (ans.transform.parent.gameObject.activeSelf) // Check if the UI is active
        {
            for (int i = 0; i <= 9; i++)
            {
                // Check for number key inputs
                if (Input.GetKeyDown(i.ToString()))
                {
                    Number(i); // Call Number method to add digit
                }
            }

            // Handle backspace key for deletion
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Delete();
            }

            // Handle Enter key for submission
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Enter();
            }
        }
    }
}
