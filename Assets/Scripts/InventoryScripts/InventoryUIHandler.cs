using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUIHandler : MonoBehaviour
{
    //CHANGES - Commented code is from original iteration. Code in use has been reworked to work with 

    //public Inventory inventory;    
    //public GameObject inventoryUIPanel;
    //public List<GameObject> inventoryUIButtons = new List<GameObject>();
    public InventoryBase inventory;
    public GameObject pagePrefab;
    public GameObject pageParent;
    public GameObject[] buttonPrefabArray;
    private int currentPageNum;

    //private void OnEnable()
    //{
    //    inventoryUIButtons.Clear();
    //    AddDesendants(inventoryUIPanel.transform, inventoryUIButtons);
    //    RefreshInventory();
    //}

    private void OnEnable()
    {
        UpdateDisplay();
        currentPageNum = 1;
        UpdateDisplayedPage(currentPageNum);
    }

    //Moving UpdateDisplay to OnEnable removes the need for CreateDisplay
    //public void Start()
    //{
    //    CreateDisplay();
    //}

    //public void Update()
    //{
    //    UpdateDisplay();
    //}

    //Moving UpdateDisplay to OnEnable removes the need for CreateDisplay
    //public void CreateDisplay()
    //{
    //    for (int i = 0; i < inventory.items.Count; i++)
    //    {
    //        GameObject pageObject = InstantiatePage(i);

    //        GameObject buttonPrefab = FindButtonPrefab(inventory.items[i].item.itemName);

    //        var obj = Instantiate(buttonPrefab, pageObject.transform.GetChild(0).transform);
    //        var buttonLabelTransform = obj.transform.Find("InventoryButtonText");
    //        if (buttonLabelTransform != null)
    //        {
    //            buttonLabelTransform.GetComponent<TextMeshProUGUI>().text = inventory.items[i].item.itemName;
    //        }
    //        var buttonAmountTransform = obj.transform.Find("InventoryButtonAmount");
    //        if (buttonAmountTransform != null)
    //        {
    //            buttonAmountTransform.GetComponent<TextMeshProUGUI>().text = inventory.items[i].amount.ToString();
    //        }

    //    }
    //}

    //Unfinished version of UpdateDisplay that was first attempt. Realised part way through that I could just clear the buttons from the inventory and then recreate it (logic being it is the same as C++ clearing the screen and re-rendering all items)
    //public void UpdateDisplay()
    //{
    //    for (int i = 0; i < inventory.items.Count; i++) 
    //    {
    //        GameObject pageObject = InstantiatePage(i);

    //        //Checks how many buttons are on the current page to define for loop
    //        var buttonParent = pageObject.transform.GetChild(0).transform;
    //        var pageButtonCount = buttonParent.childCount;            

    //        for (int j = 0; j < pageButtonCount; j++)
    //        {
    //            var button = buttonParent.GetChild(j);

    //            if(button.transform.Find("InventoryButtonText").GetComponent<TextMeshProUGUI>().text == inventory.items[i].item.itemName)
    //            {
    //                button.transform.Find("InventoryButtonAmount").GetComponent<TextMeshProUGUI>().text = inventory.items[i].amount.ToString();
    //            }
    //        }
    //    }
    //}

    public void UpdateDisplay()
    {
        //Unsure if this is performative. See above commented function for logic of choosing this one
        //foreach (Transform child in pageParent.transform)
        //{
        //    if (child.gameObject.name.Contains("Page"))
        //    {
        //        var inventoryButtonsObject = child.transform.GetChild(0);
        //        foreach (Transform child2 in inventoryButtonsObject.transform)
        //        {
        //            Destroy(child2.gameObject);
        //        }
        //    }
        //}

        foreach (Transform child in pageParent.transform)
        {
            if(child.name != "InventoryHeader")
            {
                DestroyImmediate(child.gameObject);
            }            
        }

        //Re-instantiate all buttons and instantiate new pages if necessary
        for (int i = 0; i < inventory.items.Count; i++)
        {
            GameObject pageObject = InstantiatePage(i);

            GameObject buttonPrefab = FindButtonPrefab(inventory.items[i].item.itemName);

            if(buttonPrefab == null)
            {
                Debug.Log("null button prefab");
            }
            if (pageObject.transform.GetChild(0).transform == null)
            {
                Debug.Log("PageObject child transform null");
            }

            var obj = InstantiateWithListeners(buttonPrefab, pageObject.transform.GetChild(0).transform);
            
            if (obj.name.Contains("(Clone)"))
            {
                obj.name = obj.name.Replace("(Clone)", i.ToString());
            }
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
            var buttonComponent = obj.GetComponent<Button>();
            if(buttonComponent != null)
            {
                int temp = i;
                //Debug.Log("Button " + i + " Found");
                //UnityAction dropAction = () => RemoveItem(inventory.items[i]);
                //buttonComponent.onClick.AddListener(dropAction);
                //buttonComponent.onClick.AddListener(() => { Debug.Log(temp); });
                buttonComponent.onClick.AddListener(() => { RemoveItem(inventory.items[temp]); });
            }


            //obj.GetComponent<Button>().onClick.AddListener(() => RemoveItem(inventory.items[i]));
        }

        //For loop to decide which page change arrows to show
        for (int i = 0; i < pageParent.transform.childCount; i++)
        {
            GameObject child = pageParent.transform.GetChild(i).gameObject;
            if (child.name.Contains("Page"))
            {
                string finalPage = "Page" + (pageParent.transform.childCount - 1).ToString();
                if(child.name == "Page1" && pageParent.transform.childCount - 1 > 1)
                {
                    child.transform.Find("PageForward").gameObject.SetActive(true);
                    child.transform.Find("PageBack").gameObject.SetActive(false);
                }
                else if(child.name == "Page1")
                {
                    child.transform.Find("PageForward").gameObject.SetActive(false);
                    child.transform.Find("PageBack").gameObject.SetActive(false);
                }
                else if (child.name == finalPage)
                {
                    child.transform.Find("PageForward").gameObject.SetActive(false);
                    child.transform.Find("PageBack").gameObject.SetActive(true);
                }
                else
                {
                    child.transform.Find("PageForward").gameObject.SetActive(true);
                    child.transform.Find("PageBack").gameObject.SetActive(true);
                }

                child.transform.Find("PageForward").GetComponent<Button>().onClick.AddListener(() => UpdateDisplayedPage(true));
                child.transform.Find("PageBack").GetComponent<Button>().onClick.AddListener(() => UpdateDisplayedPage(false));
            }   
        }

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

    private GameObject FindButtonPrefab(string itemName)
    {
        GameObject buttonPrefab = null;

        switch (itemName)
        {
            case "Axe":
                buttonPrefab = buttonPrefabArray[0];
                break;
            case "Bardiche":
                buttonPrefab = buttonPrefabArray[1];
                break;
            case "Bone":
                buttonPrefab = buttonPrefabArray[2];
                break;
            case "Broadsword":
                buttonPrefab = buttonPrefabArray[3];
                break;
            case "Double Ended Spear":
                buttonPrefab = buttonPrefabArray[4];
                break;
            case "Greatsword":
                buttonPrefab = buttonPrefabArray[5];
                break;
            case "Kukri":
                buttonPrefab = buttonPrefabArray[6];
                break;
            case "Shield":
                buttonPrefab = buttonPrefabArray[7];
                break;
        }

        return buttonPrefab;
    }

    private GameObject InstantiatePage(float i)
    {
        //Math to find relevant page number
        float pageNum = i / (float)30;
        pageNum = pageNum == 0 ? 1 : pageNum;
        int truePageNum = (int)Math.Ceiling(pageNum);
        //truePageNum = truePageNum == pageNum ? truePageNum + 1 : truePageNum;
        string pageToFind = "Page" + truePageNum.ToString();

        //If relevant page doesn't exist, instantiate one
        GameObject pageObject = GameObject.Find(pageToFind);
        if (pageObject == null)
        {
            pageObject = Instantiate(pagePrefab, pageParent.transform);
            pageObject.name = pageToFind;
            //pageObject.transform.Find("InventoryButtons").GetComponent<InventoryUIHandler>().pageParent = pageParent;
        }
        return pageObject;
    }

    public void UpdateDisplayedPage(bool pageIncrease)
    {
        int pageToShow = pageIncrease ? currentPageNum + 1 : currentPageNum - 1;
        Debug.Log(pageToShow.ToString());
        for (int i = 0; i < pageParent.transform.childCount; i++)
        {
            GameObject child = pageParent.transform.GetChild(i).gameObject;
            if (child.name.Contains("Page"))
            {
                string pageToFind = "Page" + pageToShow.ToString();
                if (child.name == pageToFind)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        currentPageNum = pageToShow;
    }

    public void UpdateDisplayedPage(int pageToShow)
    {
        for (int i = 0; i < pageParent.transform.childCount; i++)
        {
            GameObject child = pageParent.transform.GetChild(i).gameObject;
            if (child.name.Contains("Page"))
            {
                string pageToFind = "Page" + pageToShow.ToString();
                if(child.name == pageToFind)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        currentPageNum = pageToShow;
    }

    public void RemoveItem(InventorySlot itemToRemove)
    {
        inventory.Remove(itemToRemove);
    }

    GameObject InstantiateWithListeners(GameObject prefab, Transform parentTransform)
    {
        GameObject instance = GameObject.Instantiate(prefab, parentTransform) as GameObject;
        if (instance.GetComponent<Button>() != null)
            instance.GetComponent<Button>().onClick = prefab.GetComponent<Button>().onClick;
        return instance;
    }
}
