using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class SlotControles : MonoBehaviour
{
    public Toggle[] toggles;
    public GameObject paymentPannel;
    private StoreManager storeManager;
    [HideInInspector] public string ArticleDetails;

    void Start()
    {
        storeManager = StoreManager.instance;
        ToggleOffPaymentSection();
        LoadStoreDataQuantity();
    }
    public void ToggleOnPaymentSection()
    {
        storeManager.BuyAnItem(paymentPannel, true);
    }
    public void ToggleOffPaymentSection()
    {
        storeManager.BuyAnItem(paymentPannel, false);
    }
    public void BuyArticle()
    {
        if (toggles[0].isOn) storeManager.buy(gameObject, 0, price => price); // Coins
        else if (toggles[1].isOn) storeManager.buy(gameObject, 1, price => Mathf.RoundToInt(price / 2f)); // Gems
        else if (toggles[2].isOn) storeManager.buy(gameObject, 2, price => price * 10); // Tokens
        else Debug.Log("No payment method selected");
        SaveStoreData();
    }
    public void SaveStoreData()
    {
        string A_date = JsonUtility.ToJson(gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text);
        File.WriteAllText(Application.persistentDataPath + "/storeData.json", A_date);
        print(gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text);
    }
    public void LoadStoreDataQuantity()
    {
        if (File.Exists(Application.persistentDataPath + "/storeData.json"))
        {
            string A_data = File.ReadAllText(Application.persistentDataPath + "/storeData.json");
            A_data = JsonUtility.FromJson<string>(A_data);
            print(A_data);
        }
    }

}
