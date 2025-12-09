using UnityEngine;
using System.Collections.Generic;


public enum ItemType
{
    Weapon,
    Shield,
    Misc,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    //Holds the visual representation of the object
    public GameObject prefab;
    //Holds the item category
    public ItemType type;
    public string itemName;
    [TextArea(15,20)]
    public string description;
    public float weight;
    public float value;
}
