using UnityEngine;

public enum WeaponEnchantments
{
    None,
    Fire,
    Ice,
    Lightning,
    Dark
}

public enum WieldType
{
    MainHand,
    OffHand,
    BothHands,
    TwoHand
}

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : EquipmentObject
{
    public float damage;
    public float attackInterval;
    public float defenseBonus;
    public WeaponEnchantments enchantment;
    public WieldType wieldType;

    public void Awake()
    {
        type = ItemType.Weapon;
        materialType = MaterialType.Bronze;
        enchantment = WeaponEnchantments.None;
        wieldType = WieldType.MainHand;
    }
}
