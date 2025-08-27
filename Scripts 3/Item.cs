using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Details")]
    public string itemName; // Name of the item
    public Sprite icon; // Icon representing the item
    [TextArea(3, 10)]
    public string description; // Description of the item
}
