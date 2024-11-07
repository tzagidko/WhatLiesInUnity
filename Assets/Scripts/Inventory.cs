using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int maxSlots = 3;               // Maximum number of slots in the inventory
    private List<string> items;             // List to store item names (like "Key")

    void Start()
    {
        items = new List<string>();
    }

    // Adds an item to the inventory if there's room
    public bool AddItem(string itemName)
    {
        if (items.Count < maxSlots)
        {
            items.Add(itemName);
            Debug.Log(itemName + " added to inventory.");
            return true;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }

    // Checks if an item exists in the inventory
    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    // Removes an item from the inventory
    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            Debug.Log(itemName + " removed from inventory.");
        }
        else
        {
            Debug.Log(itemName + " is not in the inventory.");
        }
    }

    // Debug function to print inventory contents
    public void PrintInventory()
    {
        Debug.Log("Inventory contains: " + string.Join(", ", items));
    }
}