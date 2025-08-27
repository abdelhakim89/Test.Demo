using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;
public class DataManger : MonoBehaviour
{
    [Header("Data Saving System Informations")]
    [SerializeField] private TMP_InputField nameText;
    [SerializeField] private TMP_InputField ageText;
    [SerializeField] private Toggle toggle1, toggle2;
    [SerializeField] private GameObject panel;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private int Timeintervals; // Data will be saved every X seconds
    private PlayerData currentPlayerData;
    void Awake()
    {
        currentPlayerData = new PlayerData();
    }
    void Start()
    {
        loadData();
        inventoryManager = InventoryManager.Instance;
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager instance not found. Please ensure it is initialized before DataManager.");
        }
        DataSvingStarter();
    }
    private PlayerData.Gender gender =>
        toggle1.isOn ? PlayerData.Gender.Female :
        PlayerData.Gender.Male;
    private void assignData() // this method will be Invoked as soon as the player press next
    {
        if (string.IsNullOrEmpty(nameText.text) || string.IsNullOrEmpty(ageText.text) || verifyInputFields(ageText.text) == false)
        {
            Debug.LogWarning("Name or Age fields are empty. Please fill them before saving.");
            return;
        }
        currentPlayerData.playerName = nameText.text;
        currentPlayerData.playerAge = int.Parse(ageText.text);

        currentPlayerData.inventoryItems = inventoryManager.items; // Assign the inventory items
        currentPlayerData.playerGender = gender;
    }
    public void manageToggles() // Ensure only one toggle is selected
    {
        if (toggle1.isOn) toggle2.isOn = false;
        else if (toggle2.isOn) toggle1.isOn = false;
    }
    public void saveData() // Save the player data
    {
        assignData();
        toggleCanvas();

        string jsonData = JsonUtility.ToJson(currentPlayerData);
        print(jsonData);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", jsonData);
        Debug.Log("Data saved: ");
    }
    private void loadData()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            currentPlayerData = JsonUtility.FromJson<PlayerData>(jsonData);

            print(currentPlayerData.inventoryItems);
            inventoryManager.items = currentPlayerData.inventoryItems;

            Debug.Log("Data loaded: " + currentPlayerData.playerName + ", Age: " + currentPlayerData.playerAge +
                        ", Gender: " + currentPlayerData.playerGender + ", Score: " + currentPlayerData.score);
        }
    }
    public void cancelData() // Reset the input fields
    {
        nameText.text = string.Empty;
        ageText.text = string.Empty;
        toggle1.isOn = false;
        toggle2.isOn = false;
        Debug.Log("Data reset");
    }
    private void toggleCanvas()
    {
        panel.SetActive(false);
    }
    private IEnumerator DataSvingStarter()
    {
        yield return new WaitForSeconds(12f);
        StartCoroutine(DataSavingIntervals(Timeintervals));
    }
    private IEnumerator DataSavingIntervals(int Timeintervals)
    {
        while (true)
        {
            yield return new WaitForSeconds(Timeintervals);
            saveData();

            Debug.Log("Data saved at regular interval");
        }
    }
    private bool verifyInputFields(string ageText)
    {
        foreach (char c in ageText)
        {
            if (!char.IsDigit(c))
            {
                Debug.LogWarning("Age field contains non-numeric characters.");
                return false;
            }
        }
        return true;
    }
}