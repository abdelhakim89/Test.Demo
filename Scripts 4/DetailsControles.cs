using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetailsControles : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject D_Pannel;
    private void Start()
    {
       D_Pannel = GameObject.Find("Details Panel");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (D_Pannel != null) ShowDetails();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (D_Pannel != null) HideDetails();
    }
    private void ShowDetails()
    {
        D_Pannel.SetActive(true);
        D_Pannel.GetComponent<TextMeshProUGUI>().text = transform.GetComponentInParent<SlotControles>().ArticleDetails;
        Debug.LogWarning("Event Triggered");
    }
    private void HideDetails()
    {
        D_Pannel.SetActive(false);
    }

}
