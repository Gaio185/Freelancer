using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class FontChangerEditor : MonoBehaviour
{
    ////[MenuItem("Tools/Change All UI Fonts to Roboto-Bold SDF")]
    //public static void ChangeFontInScene()
    //{
    //    // Load the TMP_FontAsset (Roboto-Bold SDF) from the specific path
    //    TMP_FontAsset robotoBoldSDF = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(
    //        "Assets/TextMesh Pro/Examples & Extras/Resources/Fonts & Materials/Roboto-Bold SDF.asset");

    //    if (robotoBoldSDF == null)
    //    {
    //        Debug.LogError("Roboto-Bold SDF font not found! Please check the path.");
    //        return;
    //    }

    //    // Change TextMeshPro fonts
    //    TextMeshProUGUI[] tmpComponents = FindObjectsOfType<TextMeshProUGUI>(true);
    //    foreach (TextMeshProUGUI tmp in tmpComponents)
    //    {
    //        Undo.RecordObject(tmp, "Change TMP Font");
    //        tmp.font = robotoBoldSDF; // Assign Roboto-Bold SDF
    //        EditorUtility.SetDirty(tmp);
    //    }

    //    Debug.Log("All TMP fonts changed to Roboto-Bold SDF!");
    //}
}
