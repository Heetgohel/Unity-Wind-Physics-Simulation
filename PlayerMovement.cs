using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 8f;
    public float gravity = -20f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Camera playerCamera;
    private float verticalVelocity = 0f;
    private float cameraPitch = 0f;
    private bool isLocked = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        LockCursor();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            UnlockCursor();

        if (Mouse.current.rightButton.wasPressedThisFrame && !isLocked)
            LockCursor();

        if (isLocked)
            HandleMouseLook();

        HandleMovement();
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isLocked = true;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isLocked = false;
    }

    void HandleMouseLook()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime * 10f;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime * 10f;

        transform.Rotate(0f, mouseX, 0f);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Keyboard.current.aKey.isPressed) moveX = -1f;
        if (Keyboard.current.dKey.isPressed) moveX =  1f;
        if (Keyboard.current.sKey.isPressed) moveZ = -1f;
        if (Keyboard.current.wKey.isPressed) moveZ =  1f;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (controller.isGrounded)
            verticalVelocity = -2f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;
        controller.Move(move * walkSpeed * Time.deltaTime);
    }
}