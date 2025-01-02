using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    public void StartGame()
    {
        // Load the Loading Screen Scene
        SceneManager.LoadScene("LoadingScene"); // Replace with your Loading Scene's name
    }
}
