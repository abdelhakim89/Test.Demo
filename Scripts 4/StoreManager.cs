using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    public List<GameObject> articles;
    public GameObject articleSlot;
    public GameObject storePannel;
    public Transform goodsPannel;
    public TextMeshProUGUI coinText, gemsText, tokenText;
    [HideInInspector] public PlayerData playerData;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {
        playerData = new PlayerData();
        storePannel.SetActive(false);
        updateBalance();
    }
    private void SetTheStorePannel()
    {
        CleanStore();
        foreach (GameObject article in articles)
        {
            GameObject slot = Instantiate(articleSlot, goodsPannel);
            slot.transform.GetChild(0).GetComponent<Image>().sprite = article.GetComponent<Article>().Image;
            slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = article.GetComponent<Article>().ArticleName;
            slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = article.GetComponent<Article>().Price.ToString() + " $";
            slot.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = article.GetComponent<Article>().Quantity.ToString();
            slot.GetComponent<SlotControles>().ArticleDetails = article.GetComponent<Article>().Description;
        }
    }
    public void ToggleStore()
    {
        if (storePannel.activeSelf == false)
        {
            storePannel.SetActive(true);
            SetTheStorePannel();
        }
        else
        {
            storePannel.SetActive(false);
        }
    }
    public void BuyAnItem(GameObject paymentPanel, bool v)
    {
        ToggleOffPaymentSection();
        paymentPanel.SetActive(v);
    }
    public void buy(GameObject slot, int balanceIndex, System.Func<int, int> priceModefier)
    {
        int price = int.Parse(PriceText(slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text));
        price = priceModefier(price);
        
        int quantity = int.Parse(slot.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text);
        int balanceAmount = playerData.balance[balanceIndex];

        if (balanceAmount >= price && quantity != 0)
        {
            quantity--;
            balanceAmount -= price;
            //Debug.Log($"Item {slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text} was Purchased Successfully");
        }
        slot.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = quantity.ToString();
        playerData.balance[balanceIndex] = balanceAmount;
        updateBalance();
        DeletOnZero(slot, quantity);
    }
    public void SellAnItem()
    {
        // Implement a sell logic in here
    }
    private string PriceText(string text)
    {
        string price = "";
        foreach (char c in text)
        {
            if (char.IsDigit(c))
            {
                price += c;
            }
        }
        return price;
    }
    private void updateBalance()
    {
        coinText.text = playerData.balance[0].ToString();
        gemsText.text = playerData.balance[1].ToString();
        tokenText.text = playerData.balance[2].ToString();
    }
    private void DeletOnZero(GameObject article, int quantity)
    {
        if (quantity <= 0)
        {
            Destroy(article);
        }
    }
    private void ToggleOffPaymentSection()
    {
        int count = GameObject.FindGameObjectsWithTag("PaymentPanel").Length;
        for (int i = 0; i < count; i++)
        {
            GameObject.FindGameObjectsWithTag("PaymentPanel")[i].SetActive(false);
        }
    }
    private void CleanStore()
    {
        foreach (Transform child in goodsPannel)
        {
            Destroy(child.gameObject);
        }
    }
}
