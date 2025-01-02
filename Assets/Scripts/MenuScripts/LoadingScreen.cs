using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreenUI;       // Reference to the Canvas
    public UnityEngine.UI.Slider progressBar; // Reference to the Progress Bar
    public string gameSceneName = "Alpha";  // Name of the game scene to load
    public float visualProgressSpeed = 0.5f; // Speed at which the progress bar visually fills

    void Start()
    {
        StartCoroutine(LoadSceneAsync(gameSceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreenUI.SetActive(true); // Show the loading screen

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Prevent the scene from activating immediately

        float currentVisualProgress = 0f;

        while (!operation.isDone)
        {
            // Calculate real progress from the loading operation
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Gradually increase the visual progress to match the target progress
            while (currentVisualProgress < targetProgress)
            {
                currentVisualProgress += Time.deltaTime * visualProgressSpeed;
                progressBar.value = currentVisualProgress; // Update the progress bar
                yield return null;
            }

            // If the real progress is complete and the visual progress is also full
            if (operation.progress >= 0.9f && currentVisualProgress >= 1f)
            {
                yield return new WaitForSeconds(0.5f); // Optional delay before switching scenes
                operation.allowSceneActivation = true; // Activate the loaded scene
            }

            yield return null;
        }

        loadingScreenUI.SetActive(false); // Hide the loading screen (optional)
    }
}
