using UnityEngine;
using UnityEngine.UI;

public class ManageToggles : MonoBehaviour
{
    public Toggle toggle;
    
    public void ToggleChanged()
    {
        toggle.isOn = false;
    }
}
