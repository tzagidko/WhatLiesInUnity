using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Door door;

    [Header("References")]
    public Transform playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();

       
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        MouseLook();

        
        Move();

        
        ApplyGravity();
        if (Input.GetKeyDown(KeyCode.E) && door != null)
        {
            door.TryOpenDoor();  // Attempt to open the door if the player presses "E"
        }
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))  // If colliding with a door
        {
            door = other.GetComponent<Door>();  // Get the Door component attached to the door object
        }
    }
     private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))  // If exiting the door's collider
        {
            door = null;  // Reset the door reference
        }
    }
}