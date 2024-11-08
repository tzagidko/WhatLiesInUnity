using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName = "Key";         
    public Inventory playerInventory;      

    void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory is not assigned to the key.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && playerInventory != null)
        {
            bool addedToInventory = playerInventory.AddItem(keyName);
            if (addedToInventory)
            {
                Destroy(gameObject);  
            }
        }
    }
}