using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private TMP_Text currentObjectiveText; // UI Text for current objective

    private int objectiveIndex = 0; // Tracks the current objective

    private readonly string[] objectives = // Predefined list of objectives
    {
        "Objective: Acquire the Package.",
        "Objective: Escape the facility."
    };

    void Start()
    {
        UpdateObjectiveText(); // Display the first objective
    }

    // Update the current objective text
    private void UpdateObjectiveText()
    {
        if (objectiveIndex < objectives.Length)
        {
            currentObjectiveText.text = objectives[objectiveIndex];
        }
        else
        {
            currentObjectiveText.text = "All objectives completed!";
        }
    }

    // Trigger objective completion and update
    public void CompleteObjective()
    {
        if (objectiveIndex < objectives.Length - 1)
        {
            objectiveIndex++;
            UpdateObjectiveText();
        }
    }
}
