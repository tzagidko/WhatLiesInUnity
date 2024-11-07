using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName = "Key";         // Name of the key, can be unique if you have multiple keys
    public Inventory playerInventory;      // Reference to the player's inventory

    void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory is not assigned to the key.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player interacts with the key
        if (other.CompareTag("Player") && playerInventory != null)
        {
            bool addedToInventory = playerInventory.AddItem(keyName);
            if (addedToInventory)
            {
                Destroy(gameObject);  // Destroy the key object once picked up
            }
        }
    }
}