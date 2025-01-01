using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectCapture : MonoBehaviour
{
    public Camera renderCamera; // Reference to the camera rendering the object
    public string savePath = "Assets/Resources/ItemIcons"; // Path to save the PNGs

    void Start()
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        // Capture screenshot
        CaptureScreenshot();
    }

    void CaptureScreenshot()
    {
        // Set the camera's target texture to None (no render texture)
        renderCamera.targetTexture = null;

        // Create a temporary RenderTexture with transparency support (ARGB32)
        RenderTexture temporaryTexture = new RenderTexture(516, 516, 24); // 256x256 texture size
        temporaryTexture.format = RenderTextureFormat.ARGB32; // Supports alpha channel
        renderCamera.targetTexture = temporaryTexture;

        // Set the camera's background color to transparent (Alpha = 0)
        renderCamera.backgroundColor = new Color(0, 0, 0, 0);  // Transparent background
        renderCamera.clearFlags = CameraClearFlags.SolidColor;  // Clear with solid color (transparent)

        // Render the camera's view into the temporary texture
        renderCamera.Render();

        // Set the active render texture back to the main screen
        RenderTexture.active = temporaryTexture;

        // Create a Texture2D to hold the pixels from the camera's render
        Texture2D texture = new Texture2D(temporaryTexture.width, temporaryTexture.height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, temporaryTexture.width, temporaryTexture.height), 0, 0);
        texture.Apply();

        // Encode the texture to PNG (with transparency)
        byte[] bytes = texture.EncodeToPNG();

        // Save the PNG file
        string filePath = Path.Combine(savePath, "ItemIcon_" + System.Guid.NewGuid() + ".png");
        File.WriteAllBytes(filePath, bytes);

        // Clean up
        RenderTexture.active = null;  // Clear the active render texture
        Destroy(texture);  // Destroy temporary texture
        Destroy(temporaryTexture);  // Destroy the temporary render texture

        Debug.Log("Captured transparent screenshot saved to: " + filePath);
    }
}
