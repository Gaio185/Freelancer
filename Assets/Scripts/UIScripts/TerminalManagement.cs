using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManagement : MonoBehaviour
{
    public TMP_InputField input;

    public GameObject passwordInterface;
    public GameObject workspaceInterface;

    private string correctPassword = "password";
    
    public void CheckPassword()
    {
        if (input.text == correctPassword)
        {
            passwordInterface.SetActive(false);
            workspaceInterface.SetActive(true);
            Debug.Log("Correct Password");
        }
        else
        {
            Debug.Log("Incorrect Password");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
