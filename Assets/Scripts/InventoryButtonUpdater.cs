using TMPro;
using UnityEngine;

public class InventoryButtonUpdater : MonoBehaviour
{
    public TMP_Text buttonText;

    public void SetButtonText(CollectibleObject item)
    {
        buttonText.text = item.itemName;
    }
}
