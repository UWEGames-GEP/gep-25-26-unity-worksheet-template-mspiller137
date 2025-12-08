using UnityEngine;

public enum MaterialType
{
    Bronze,
    Iron,
    Steel,
    Orichalcum,
    Starsteel
}

public class EquipmentObject : ItemObject
{
    public MaterialType materialType;
}
