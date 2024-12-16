using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerDetect : MonoBehaviour
{
    public float radius = 3f;
    public LayerMask targetMask;
    public GameObject weaponHolder;
    private bool isInRange;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInRange)
        {
            player.switchWeapon.hasWeapon = true;
            gameObject.SetActive(false);
            //weaponHolder.SetActive(true);
            player.interactPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.interactionText.text = "Press F to Pick Up";
            player.interactPanel.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.interactPanel.SetActive(false);
            isInRange = false;
        }
    }

}
