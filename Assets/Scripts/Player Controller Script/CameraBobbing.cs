using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool _enable = true;  // Toggle to enable or disable bobbing
    [SerializeField, Range(0, 0.1f)] private float _Amplitude = 0.015f;  // Amplitude of bobbing (height)
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;  // Frequency of bobbing (speed)
    [SerializeField] private Transform _camera = null;  // The camera that should bob
    [SerializeField] private float _mouseSensitivityHorizontal = 100f; // Sensitivity for horizontal mouse movement
    [SerializeField] private float _mouseSensitivityVertical = 150f;   // Sensitivity for vertical mouse movement (adjusted for speed)
    [SerializeField] private GameObject _hud = null;  // HUD reference, assignable in the Inspector

    [Header("Objects to Bob")]
    [SerializeField] private List<Transform> _bobbingObjects = new List<Transform>(); // Objects to bob along with the camera
    [SerializeField, Range(0, 0.1f)] private float _objectAmplitudeMultiplier = 1.0f; // Multiplier for object bobbing amplitude

    private float _toggleSpeed = 3.0f;  // Speed threshold to trigger bobbing
    private Vector3 _startPos;  // Initial position of the camera
    private CharacterController _controller;  // Reference to the CharacterController

    private Dictionary<Transform, Vector3> _initialPositions = new Dictionary<Transform, Vector3>(); // Store initial positions of bobbing objects
    private float _xRotation = 0f;  // For storing vertical rotation
    

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();  // Get the CharacterController from the player
        _startPos = _camera.localPosition;  // Store the initial camera position
        Cursor.lockState = CursorLockMode.Locked;  // Lock cursor for gameplay

        // Log a warning if HUD is not assigned
        if (_hud == null)
        {
            Debug.LogWarning("HUD is not assigned. Please assign it in the Inspector.");
        }

        // Store initial positions of all bobbing objects
        foreach (var obj in _bobbingObjects)
        {
            if (obj != null)
            {
                _initialPositions[obj] = obj.localPosition;
            }
        }
    }


    void Update()
    {
        if (!_enable) return;  // If bobbing is disabled, do nothing
        //HandleMouseInput(); // Handle camera rotation
        CheckMotion();  // Check if the player is moving and grounded
        ResetPosition();  // Reset camera and objects to their starting positions when not moving
    }

    // Handles vertical and horizontal rotation from mouse input
    private void HandleMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivityHorizontal * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivityVertical * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); // Clamp vertical rotation

        _camera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f); // Apply vertical rotation
        transform.Rotate(Vector3.up * mouseX); // Rotate player horizontally
    }

    // Generates footstep motion for up/down and side-to-side bobbing
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _Amplitude;  // Vertical motion (up/down)
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _Amplitude * 2;  // Horizontal sway (side-to-side)
        return pos;
    }

    // Check if the player is moving and grounded
    private void CheckMotion()
    {
        // Calculate the speed based on the horizontal velocity of the player
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;

        // If the player is moving too slowly or is in the air, skip the bobbing
        if (speed < _toggleSpeed) return;
        if (!_controller.isGrounded) return;

        // Apply the bobbing motion
        PlayMotion(FootStepMotion());
    }

    // Apply the bobbing motion to the camera and bobbing objects
    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;  // Modify the camera's local position

        foreach (var obj in _bobbingObjects)
        {
            if (obj != null)
            {
                obj.localPosition += motion * _objectAmplitudeMultiplier; // Apply scaled motion to each object
            }
        }
    }

    // Smoothly reset the camera and objects' positions when the player is not moving
    private void ResetPosition()
    {
        // If the camera's local position is already at the start position, do nothing
        if (_camera.localPosition != _startPos)
        {
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
        }

        // Reset each bobbing object's position to its initial state
        foreach (var obj in _bobbingObjects)
        {
            if (obj != null && _initialPositions.ContainsKey(obj))
            {
                obj.localPosition = Vector3.Lerp(obj.localPosition, _initialPositions[obj], 1 * Time.deltaTime);
            }
        }
    }
}
