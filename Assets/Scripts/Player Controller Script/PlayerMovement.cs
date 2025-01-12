using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    public bool hasClearance;
    public bool isHunted;
    public bool canExtract;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private AudioSource audioSource;

    public bool canMove = true;

    // Sons para caminhada, corrida e aterrissagem
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip landSound;

    // Variáveis para o controle de áudio
    private bool isGroundedLastFrame = true;
    private bool isRunning = false;
    private bool isWalking = false;

    private bool isJumping = false;

    // Variáveis para o Lean
    public float amt = 10f; // Quantidade de lean
    public float slerpAmt = 10f; // Velocidade de transição do lean
    private Quaternion initialRotation;
    private float targetZ;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        initialRotation = transform.localRotation;  // Salva a rotação inicial
        targetZ = initialRotation.eulerAngles.z;    // Define a rotação inicial no eixo Z
    }

    void Update()
    {
        // Movimentação do jogador
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunningInput = Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded;
        float curSpeedX = canMove ? (isRunningInput ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunningInput ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
            isJumping = true;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftControl) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Olhando ao redor com o mouse
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Leaning: Verifica se o jogador está tentando inclinar
        if (canMove) LeanCheck();

        // Controlando os sons de movimento e aterrissagem
        HandleFootsteps(isRunningInput);
        HandleLanding();
    }

    private void HandleFootsteps(bool isRunningInput)
    {
        bool isMoving = (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && characterController.isGrounded;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                AudioClip stepSound = isRunningInput ? runSound : walkSound;
                audioSource.clip = stepSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void HandleLanding()
    {
        if (!isGroundedLastFrame && characterController.isGrounded)
        {
            PlaySound(landSound); // Toca o som de aterrissar quando o jogador toca no solo
        }
        isGroundedLastFrame = characterController.isGrounded;
    }

    private void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    // Função para o Lean
    void LeanCheck()
    {
        Vector3 currentEulerAngles = transform.localEulerAngles;

        if (Input.GetKey(KeyCode.Q))
        {
            targetZ = initialRotation.eulerAngles.z + amt; // Inclinar para a esquerda
        }
        else if (Input.GetKey(KeyCode.E))
        {
            targetZ = initialRotation.eulerAngles.z - amt; // Inclinar para a direita
        }
        else
        {
            targetZ = initialRotation.eulerAngles.z; // Voltar à rotação inicial
        }

        currentEulerAngles.z = Mathf.LerpAngle(currentEulerAngles.z, targetZ, Time.deltaTime * slerpAmt);
        transform.localRotation = Quaternion.Euler(currentEulerAngles);
    }
}