using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

//IMPORTANT - Inventory stored as scriptable object to allow for creation of more inventories
//e.g. different loadouts for different classes that the player could choose
//e.g. enemy inventories, shop inventories, chest inventories, etc


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryBase : ScriptableObject
{
    public List<InventorySlot> items = new List<InventorySlot>();

    //Original AddItem with only item as parameter designed for picking up from ground
    //This AddItem should work elsewhere
    public void AddItem(ItemObject _item, int _amount)
    {
        //initial assumption that we do not have the item
        bool hasItem = false;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == _item)
            {
                items[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            items.Add(new InventorySlot(_item, _amount));
        }
    }
    //AddItem with item, giver and receiever designed for item transfer between two inventories (player, chest, weapon rack, etc)
    public virtual void AddItem(ItemObject _item, int _amount, InventoryBase giver, InventoryBase receiver) { }


    //Remove function with InventorySlot parameter is the base remove function. Use of this function will drop the item in front of the player. Overrides of this function route back to it
    public bool Remove(InventorySlot item)
    {
        bool toDestroy = false;

        Transform playerTransform = GameObject.Find("Skeleton_110").gameObject.transform;
        Vector3 playerLoc = playerTransform.position;
        Vector3 playerFor = playerTransform.forward;

        Vector3 itemLoc = playerLoc + playerFor * 2;
        itemLoc.y = 1;

        Quaternion playerRot = playerTransform.rotation;
        Quaternion itemRot = playerRot * Quaternion.Euler(0, 180, 0);

        Transform collectiblesTransform = GameObject.Find("Collectibles").gameObject.transform;
        GameObject newItem = Instantiate(item.item.prefab, itemLoc, itemRot, collectiblesTransform);
        newItem.SetActive(true);

        if (item.amount == 1)
        {
            items.Remove(item);
            toDestroy = true;
        }
        else
        {
            item.amount--;
        }
        return toDestroy;

    }
    //Remove function with no parameters is to remove top item from list with a key press -- **DEPRECATED**
    public virtual void Remove() { }
    //Remove function with int parameter to drop item from player inventory. int is button index that relates to items list
    public void Remove(string buttonName)
    {
        bool toDestroy = false;
        int buttonNum = 0;
        if(!Int32.TryParse(buttonName.Replace("InventoryButton", ""), out buttonNum))
        {
            Debug.Log("Int conversion failed");
        }

        if (buttonNum < items.Count)
        {
            //toDestroy = true;
            toDestroy = Remove(items[buttonNum]);
        }

        if (toDestroy)
        {
            GameObject buttonToDestroy = GameObject.Find(buttonName).gameObject;
            Destroy(buttonToDestroy);
        }
    }


    //Potentially remove from inventory???
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ItemObject collisionItem = hit.gameObject.GetComponent<ItemObject>();

        if (collisionItem != null)
        {
            InventorySlot newSlot = new InventorySlot(collisionItem, 1);
            items.Add(newSlot);
            //collisionItem.SetActive(false);
        }
    }
}

//Serializable so visible in editor
[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    //Added underscore to parameter to add encapsulation, same as in original Inventory.cs item list
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int val)
    {
        amount += val;
    }


}
