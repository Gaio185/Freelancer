using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    private bool isStunned = false;

    // This method is called when the enemy takes damage
    public void TakeDamage(float amount)
    {
        // Reduce health by the damage amount
        health -= amount;

        // Check if the health drops to or below 0
        if (health <= 0f && !isStunned)
        {
            Stun();
        }
    }

    // Stun the enemy instead of destroying
    void Stun()
    {
        // Set isStunned to true so the enemy won't get stunned multiple times
        isStunned = true;

        // Log to the console that the enemy is stunned
        Debug.Log("Enemy is stunned");

        // You could add further behavior for the stunned state here
        // For example, disable enemy movement or actions while stunned
    }
}
