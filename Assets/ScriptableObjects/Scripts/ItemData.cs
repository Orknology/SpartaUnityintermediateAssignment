using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    //���� ��ȭ�� ���ǰ
    Weapon,
    Armor,
    //ü�� ȸ���� �Ҹ�ǰ
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
    //ü�� ȸ�� �����۸� ���� ���̱⿡ Ÿ�� �з� ���� ����
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
