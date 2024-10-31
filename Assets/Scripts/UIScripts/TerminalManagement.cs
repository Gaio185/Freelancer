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
    public TOAgent[] tOAgents;

    public Button cameraOnButton;
    public Button cameraOffButton;

    public Button sentryOnButton;
    public Button sentryOffButton;

    public GameObject player;

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
            input.text = "";
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
        if (Input.GetKey(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerMovement>().canMove = true;
        }

        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Camera")
            {
                if (tOAgents[i].stateMachine.currentState == TOStateId.On)
                {
                    cameraOnButton.interactable = false;
                    cameraOffButton.interactable = true;
                }
                else
                {
                    cameraOnButton.interactable = true;
                    cameraOffButton.interactable = false;
                }
            }
            else if (tOAgents[i].tag == "Sentry")
            {
                if (tOAgents[i].stateMachine.currentState == TOStateId.Off)
                {
                    sentryOnButton.interactable = true;
                    sentryOffButton.interactable = false;
                }
                else
                {
                    sentryOnButton.interactable = false;
                    sentryOffButton.interactable = true;
                }
            }

        }
    }

    public void CameraOnState()
    {
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Camera")
            {
                tOAgents[i].stateMachine.ChangeState(TOStateId.On);
            }
        }
    }

    public void CameraOffState()
    {
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Camera")
            {
                tOAgents[i].stateMachine.ChangeState(TOStateId.Off);
            }
        }
    }

    public void SentryOnState()
    {
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Sentry")
            {
                tOAgents[i].stateMachine.ChangeState(TOStateId.On);
            }
        }
    }

    public void SentryOffState()
    {
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Sentry")
            {
                tOAgents[i].stateMachine.ChangeState(TOStateId.Off);
            }
        }
    }
}
