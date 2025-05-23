using UnityEngine;

public enum itemType
{
    Consumable,
}

public enum ConsumableType
{
    Health,
    stamina,
    destroyObject,
    buff
}

[System.Serializable]
public class itemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "item", menuName = "New item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    public string displayName;
    public string description;
    public itemType type;
    public ConsumableType consumData;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount = 99;

    [Header("Consumable")]
    public itemDataConsumable[] consumables;
}
