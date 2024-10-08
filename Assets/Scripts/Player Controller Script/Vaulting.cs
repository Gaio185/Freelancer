using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaulting : MonoBehaviour
{
    private int vaultLayer;
    public Camera cam;
    private float playerHeight = 2f;
    private float playerRadius = 0.5f;

    void Start()
    {
        vaultLayer = 1 << LayerMask.NameToLayer("VaultLayer");  // Fix layer mask setup
    }

    void Update()
    {
        Vault();
    }

    private void Vault()
    {
        float cameraAngle = Vector3.Dot(cam.transform.forward, Vector3.up);

        // Prevent vaulting if the player is looking too far down or too far up
        if (cameraAngle < 0.8f && cameraAngle > -0.8f)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Adjust the ray origin slightly higher to avoid hitting unwanted objects when looking down
                Vector3 rayOrigin = transform.position + Vector3.up * (0.5f * playerHeight);

                if (Physics.Raycast(rayOrigin, cam.transform.forward, out var firstHit, 0.8f, vaultLayer))
                {
                    print("vaultable in front");

                    // Cast the second ray to find a landing position
                    if (Physics.Raycast(firstHit.point + (cam.transform.forward * playerRadius) + (Vector3.up * 0.6f * playerHeight), Vector3.down, out var secondHit, playerHeight))
                    {
                        print("found place to land");
                        StartCoroutine(LerpVault(secondHit.point, 0.5f));
                    }
                }
            }
        }
    }

    IEnumerator LerpVault(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
