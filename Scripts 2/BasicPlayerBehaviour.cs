using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string playerName;
    public int playerAge;
    public enum Gender {Male, Female};
    public Gender playerGender;
    public int score;
    public List<Item> inventoryItems;
    public int[] balance = {500, 800, 200};
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}

public class BasicPlayerBehaviour : MonoBehaviour
{
    private Rigidbody rb; // Reference to the player's Rigidbody component
    private float moveSpeed = 5f; // Speed of the player movement
    private float HorizontalInput; // Horizontal input value
    private float VerticalInput; // Vertical input value

    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrows)
        VerticalInput = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrows)
        Vector3 movement = new Vector3(HorizontalInput, 0, VerticalInput).normalized; // Create a movement vector
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        InventoryManager.Instance.AddItem(collision.gameObject.GetComponent<Item>()); // Add item to inventory
        Destroy(collision.gameObject);
        Debug.Log("Item collected: " + collision.gameObject.GetComponent<Item>().itemName);
    }
    
}
