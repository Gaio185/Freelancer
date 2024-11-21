using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    private Player player;
    public GameObject noteSelectorMenu;
    public GameObject noNotesMenu;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerRef = GameObject.FindGameObjectWithTag("Player");
        player = playerRef.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.movement.canMove = true;
            player.switchWeapon.disableTools = false;
            player.HUD.SetActive(true);
        }

        if(player.notes.Count > 0)
        {
            if (noNotesMenu)
            {
                noteSelectorMenu.SetActive(true);
                noNotesMenu.SetActive(false);
            }
        }
    }
}
