using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour
{
    //CHANGES - Commented code is from original iteration. Code in use has been reworked to work with 

    //public Inventory inventory;    
    //public GameObject inventoryUIPanel;
    //public List<GameObject> inventoryUIButtons = new List<GameObject>();
    public InventoryBase inventory;
    public GameObject pagePrefab;
    public GameObject pageParent;

    //private void OnEnable()
    //{
    //    inventoryUIButtons.Clear();
    //    AddDesendants(inventoryUIPanel.transform, inventoryUIButtons);
    //    RefreshInventory();
    //}

    public void Start()
    {
        CreateDisplay();
    }

    public void Update()
    {
        //UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            float pageNum = i / 30;
            int truePageNum = (int)Math.Ceiling(pageNum);
            string pageToFind = "Page" + truePageNum.ToString();
            GameObject pageObject = GameObject.Find(pageToFind);
            if (pageObject == null)
            {
                pageObject = Instantiate(pagePrefab);
            }


            var obj = Instantiate(inventory.items[i].item.prefab);
            var buttonLabelTransform = obj.transform.Find("InventoryButtonText");
            if (buttonLabelTransform != null)
            {
                buttonLabelTransform.GetComponent<TextMeshProUGUI>().text = inventory.items[i].item.itemName;
            }
            var buttonAmountTransform = obj.transform.Find("InventoryButtonAmount");
            if (buttonAmountTransform != null)
            {
                buttonAmountTransform.GetComponent<TextMeshProUGUI>().text = inventory.items[i].amount.ToString();
            }

        }
    }

    public void UpdateDisplay()
    {

    }


    /*
    private void RefreshInventory()
    {
        //Debug.Log(inventoryUIButtons.Count);
        foreach (GameObject button in inventoryUIButtons) 
        {
            button.SetActive(false);
        }
        //Debug.Log("Post-foreach");

        for(int i = 0; i < inventory.items.Count; i++)
        {
            if(i < inventoryUIButtons.Count)
            {
                var button = inventoryUIButtons[i].GetComponent<InventoryButtonUpdater>();
                var item = inventory.items[i];

                button.gameObject.SetActive(true);
                button.SetButtonText(item.item);
            }
        }
        //Debug.Log("Refresh Inventory");
    }

    public void OnInventoryUIButton(int buttonNum)
    {
        inventory.Remove(buttonNum);
        RefreshInventory();
    }
    */
    private void AddDesendants(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "Button")
            {
                list.Add(child.gameObject);
            }
        }
    }
}
