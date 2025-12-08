using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour
{
    public Inventory inventory;
    public GameObject inventoryUIPanel;
    public List<GameObject> inventoryUIButtons = new List<GameObject>();

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        inventoryUIButtons.Clear();
        AddDesendants(inventoryUIPanel.transform, inventoryUIButtons);
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        //Debug.Log(inventoryUIButtons.Count);
        foreach (GameObject button in inventoryUIButtons) 
        {
            button.SetActive(false);
        }
        //Debug.Log("Post-foreach");

        for(int i = 0; i < inventory._items.Count; i++)
        {
            if(i < inventoryUIButtons.Count)
            {
                var button = inventoryUIButtons[i].GetComponent<InventoryButtonUpdater>();
                var item = inventory._items[i];

                button.gameObject.SetActive(true);
                button.SetButtonText(item);
            }
        }



        Debug.Log("Refresh Inventory");
    }

    public void OnInventoryUIButton(int buttonNum)
    {
        inventory.Remove(buttonNum);
        RefreshInventory();
    }

    private void AddDesendants(Transform parent, List<GameObject> list)
    {
        foreach(Transform child in parent)
        {
            if(child.gameObject.tag == "Button")
            {
                list.Add(child.gameObject);
            }            
        }
    }
}
