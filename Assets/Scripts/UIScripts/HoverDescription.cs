using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string description; // Each button has its own unique description

    public GameObject descriptionPanel; // Each button has its own description panel
    public TMP_Text descriptionText;    // Each button has its own TextMeshPro component for description

    private bool isHovering = false;    // Track whether the mouse is hovering over the button

    void Start()
    {
        // Ensure the description panel is hidden at the start
        descriptionPanel.SetActive(false);

        // Add the onClick listener to hide the description panel when the button is clicked
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Start hovering
        isHovering = true;

        // Update the description text for this specific button
        descriptionText.text = description;

        // Make the panel visible when hovering
        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Stop hovering
        isHovering = false;

        // Hide the panel when the mouse exits the button area
        descriptionPanel.SetActive(false);
    }

    void Update()
    {
        // If hovering, position the panel slightly above the cursor
        if (isHovering)
        {
            Vector3 mousePosition = Input.mousePosition;

            // Slightly adjust the Y position to place it above the cursor
            float panelYOffset = 30f;  // Adjust this value for positioning the panel just above the cursor
            descriptionPanel.transform.position = new Vector3(mousePosition.x, mousePosition.y + panelYOffset, mousePosition.z);
        }
    }

    // This method is called when the button is clicked
    void OnButtonClick()
    {
        // Hide the description panel when the button is clicked
        descriptionPanel.SetActive(false);
    }
}