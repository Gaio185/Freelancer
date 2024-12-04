using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerMovement movement;
    public Switchweapon switchWeapon;
    public PauseMenu pauseMenu;

    public List<Keycard> keycards;
    public List<GameObject> keycardUI;

    public List<GameObject> notes;
    public List<GameObject> noteUI;

    public GameObject noteInterface;
    public GameObject HUD;
    public GameObject interactPanel;
    public TextMeshProUGUI interactionText;

    public bool canPause;


    // Start is called before the first frame update
    void Start()
    {
        keycards = new List<Keycard>();
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        switchWeapon = GetComponent<Switchweapon>();
        pauseMenu = GetComponent<PauseMenu>();
        canPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < keycards.Count; i++)
        {
            for(int j = 0; j < keycardUI.Count; j++)
            {
                if(keycards[i].GetDivisionType().ToString() == keycardUI[j].name && !keycardUI[j].activeSelf)
                {
                    keycardUI[j].SetActive(true);
                }
            }
        }

        for (int i = 0; i < notes.Count; i++)
        {
            for (int j = 0; j < noteUI.Count; j++)
            {
                if (notes[i].name == noteUI[j].name && !noteUI[j].activeSelf)
                {
                    noteUI[j].SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.J) && movement.canMove)
        {
            canPause = false; // Disable pause
            HUD.SetActive(false);
            Cursor.visible = true; // Show cursor
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            movement.canMove = false; // Disable player movement
            switchWeapon.disableTools = true; // Disable player tools
            switchWeapon.DeactivateAllModels();
            noteInterface.SetActive(true);
        }
    }
}
