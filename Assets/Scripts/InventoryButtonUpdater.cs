using TMPro;
using UnityEngine;

public class InventoryButtonUpdater : MonoBehaviour
{
    //Potentially Deprecated - unable to acquire text child of button prefab to apply function
    public TMP_Text buttonText;

    public void SetButtonText(ItemObject item)
    {
        buttonText.text = item.itemName;
    }
}
