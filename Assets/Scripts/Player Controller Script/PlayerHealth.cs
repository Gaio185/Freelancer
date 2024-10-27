using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
