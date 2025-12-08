using UnityEngine;
using System.Collections.Generic;

public abstract class InventoryBase : MonoBehaviour
{
    

    private List<CollectibleObject> items = new List<CollectibleObject>();
    public List<CollectibleObject> _items = new List<CollectibleObject>();


    void Update()
    {
        _items = items;

        //When items is List<Object>, can use .sort(Comparison<T>) to sort by individual attributes
        //IComparer<CollectibleObject> comparer = CollectibleObject.name;
        //_items.Sort();
    }

    //AddItem with only item as parameter designed for picking up from ground
    public virtual void AddItem(CollectibleObject item) {}
    //AddItem with item, giver and receiever designed for item transfer between two inventories (player, chest, weapon rack, etc)
    public virtual void AddItem(CollectibleObject item, InventoryBase giver, InventoryBase receiver) { }


    //Remove function with item parameter is the base remove function. Use of this function will drop the item in front of the player. Overrides of this function route back to it
    public virtual void Remove(CollectibleObject item) { }
    //Remove function with no parameters is to remove top item from list with a key press -- **DEPRECATED**
    public virtual void Remove() { }
    //Remove function with int parameter to drop item from player inventory. int is button index that relates to items list
    public virtual void Remove(int buttonNum) { }


    //Potentially remove from inventory???
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CollectibleObject collisionItem = hit.gameObject.GetComponent<CollectibleObject>();

        if (collisionItem != null)
        {
            items.Add(collisionItem);
            collisionItem.gameObject.SetActive(false);
        }
    }
}
