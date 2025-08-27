using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform itemSlotContainer; // Reference to the inventory panel UI
    public GameObject itemSlotPrefab; // Prefab for the item slot UI element
    [HideInInspector]
    public static InventoryManager Instance; // Singleton instance of the InventoryManager
    public List<Item> items; // List to hold items Data in the inventory
    void Awake()
    {
        items = new List<Item>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    public void toggelInventory()
    {
        if (inventoryPanel != null)
        {
            if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
            }
            else
            {
                inventoryPanel.SetActive(true);
                clearInventory();
                setInventory();
            }
        }
        else
        {
            Debug.LogWarning("Inventory panel is not assigned in the InventoryManager.");
        }
    }
    public void AddItem(Item item)
    {
        if (itemAlreadyExists(item))
        {
            // If the item already exists, increase its quantity
            Item existingItem = items.Find(i => i.itemName == item.itemName);
        }
        else
        {
            items.Add(item);
        }
    }
    private bool itemAlreadyExists(Item item)
    {
        foreach (Item item1 in items)
        {
            if (item1.itemName == item.itemName)
            {
                return true; // Item already exists in the inventory
            }
        }
        return false; // Item does not exist in the inventory
    }
    private void setInventory()
    {
        foreach (Item item in items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            itemSlot.transform.GetChild(0).GetComponent<Image>().sprite = item.icon; // Assuming the first child is an Image component for the icon
            itemSlot.transform.GetChild(1).GetComponent<Text>().text = item.itemName; // Assuming the second child is a Text component for the item name                                                        
        }
    }
    private void clearInventory()
    {
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject); // Clear existing item slots
        }
    }
}
