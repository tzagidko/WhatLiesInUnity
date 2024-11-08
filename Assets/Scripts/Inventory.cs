using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int maxSlots = 3;               
    private List<string> items;             

    void Start()
    {
        items = new List<string>();
    }

  
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

    
    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    
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

    
    public void PrintInventory()
    {
        Debug.Log("Inventory contains: " + string.Join(", ", items));
    }
}