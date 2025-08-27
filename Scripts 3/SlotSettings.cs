using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class RemoveItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject selecter;
    private InventoryManager inventoryManager = InventoryManager.Instance;
    private Image discriptionIcon;
    private Text discriptionPanel;
    void Start()
    {
        discriptionIcon = GameObject.Find("itemImage").GetComponent<Image>();
        discriptionPanel = GameObject.Find("itemDescription").GetComponent<Text>();
    }

    public void removeItemFromInventory()
    {
        if (inventoryManager != null)
        {

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("InventoryManager instance is not available.");
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        showDetails();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hideDetails();
    }
    private void showDetails()
    {
        selecter.SetActive(true);
        Text nameText = transform.GetChild(1).GetComponent<Text>();

        foreach (Item item in inventoryManager.items)
        {
            if (item.itemName.Trim() == nameText.text.Trim())
            {
                discriptionIcon.sprite = item.icon; // Set the icon in the description panel
                discriptionPanel.text = item.description; // Set the description text
                break; // Exit loop once the item is found
            }
            else
            {
                discriptionIcon.sprite = null; // Clear icon if item not found
                discriptionPanel.text = "No description available."; // Default message
            }
        }
    }
    private void hideDetails()
    {
        selecter.SetActive(false);
        discriptionIcon.sprite = null; // Clear icon
        discriptionPanel.text = ""; // Clear description text
    }
    private Item configureItem()
    {
        Text nameText = transform.GetChild(1).GetComponent<Text>();
        foreach (Item item in inventoryManager.items)
        {
            if (item.itemName.Trim() == nameText.text.Trim())
            {
                return item;
            }
        }
        return null; // Return null if item not found
    }
  
}
