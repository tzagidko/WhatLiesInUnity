using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;                 // Tracks if the door is open
    public string requiredItem = "Key";         // Name of the item required to open the door
    public float openRotationAngle = 90f;       // How much the door rotates when opening
    public float openSpeed = 2f;                // Speed of the door opening
    public GameObject gameManager;              // Reference to the Game Manager for win state
    public Inventory playerInventory;           // Reference to the player's inventory

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, transform.eulerAngles.y + openRotationAngle, 0);
    }

    void Update()
    {
        if (isOpen)
        {
            // Smoothly rotate the door to the open position
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
    }

    // This method checks if the player has the key and opens the door if so
    public void TryOpenDoor()
    {
        if (playerInventory.HasItem(requiredItem))  // Check if the player has the key
        {
            isOpen = true;
           // gameManager.GetComponent<GameManager>().WinGame();  // Trigger win condition
            Debug.Log("Door opened, you win!");
        }
        else
        {
            Debug.Log("You need the key to open this door.");
        }
    }
}