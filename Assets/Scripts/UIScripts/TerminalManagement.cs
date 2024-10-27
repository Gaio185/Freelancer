using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManagement : MonoBehaviour
{
    public TMP_InputField input;

    public GameObject computerInterface;
    public GameObject passwordInterface;
    public GameObject workspaceInterface;

    public GameObject player;

    public static bool isComputerActive = false;

    public float radius = 10f;
    public LayerMask targetMask;

    private string correctPassword = "password";

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

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
        if (!isComputerActive)
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
            if (rangeChecks.Length > 0)
            {
                Debug.Log("player in range");
                if (Input.GetKey(KeyCode.F))
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    player.GetComponent<PlayerMovement>().canMove= false;
                    computerInterface.SetActive(true);
                    isComputerActive = true;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                computerInterface.SetActive(false);
                player.GetComponent<PlayerMovement>().canMove = true;
                isComputerActive = false;   
            }
        }
    }
}
