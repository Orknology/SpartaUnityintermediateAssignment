using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    //스탯 변화용 장비품
    Weapon,
    Armor,
    //체력 회복용 소모품
    Consumable
}

public enum ValueType
{
    Attack,
    Defense,
    Health
}

[System.Serializable]

public class ItemDataEquipableStats
{
    public float statsValue;
}

[System.Serializable]
public class ItemDataConsumable
{
    //체력 회복 아이템만 넣을 것이기에 타입 분류 딱히 안함
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItemData")]

public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("ValueType")]
    public ValueType valueType;

    [Header("ItemStats")]
    public ItemDataEquipableStats[] stats;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
}
