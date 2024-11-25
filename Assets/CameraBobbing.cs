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

    private float _toggleSpeed = 3.0f;  // Speed threshold to trigger bobbing
    private Vector3 _startPos;  // Initial position of the camera
    private CharacterController _controller;  // Reference to the CharacterController

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();  // Get the CharacterController from the player
        _startPos = _camera.localPosition;  // Store the initial camera position
    }

    void Update()
    {
        if (!_enable) return;  // If bobbing is disabled, do nothing

        CheckMotion();  // Check if the player is moving and grounded
        ResetPosition();  // Reset camera to its starting position when not moving
        _camera.LookAt(FocusTarget());  // Make camera focus ahead of the player
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

    // Apply the bobbing motion to the camera's local position
    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;  // Modify the camera's local position
    }

    // Focus the camera ahead of the player (15 units in front)
    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pos += transform.forward * 15.0f;  // Position the focus point 15 units ahead
        return pos;
    }

    // Smoothly reset the camera position when the player is not moving
    private void ResetPosition()
    {
        // If the camera's local position is already at the start position, do nothing
        if (_camera.localPosition == _startPos) return;

        // Smoothly return the camera to its original position if the player is idle
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }
}
