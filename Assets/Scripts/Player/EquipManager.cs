using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public GameObject curEquipWeapon;
    public GameObject curEquipArmor;
    public Transform equipParent;

    private PlayerStats playerStats;

    // singleton
    public static EquipManager instance;

    private void Awake()
    {
        instance = this;
        playerStats = GetComponent<PlayerStats>();
    }
    public void EquipNew(ItemData item)
    {
        if (item.type == ItemType.Weapon) 
        {
            EquipStatAdd(item);
            UnEquipWeapon();
            curEquipWeapon = Instantiate(item.equipPrefab, equipParent) as GameObject;
        }
        if (item.type == ItemType.Armor)
        {
            EquipStatAdd(item);
            UnEquipArmor();
            curEquipArmor = Instantiate(item.equipPrefab, equipParent) as GameObject;
        }
    }

    public void UnEquipWeapon()
    {
        if (curEquipWeapon != null)
        {
            Destroy(curEquipWeapon.gameObject);
            curEquipWeapon = null;
        }
    }
    public void UnEquipArmor()
    {
        if (curEquipArmor != null)
        {
            Destroy(curEquipArmor.gameObject);
            curEquipArmor = null;
        }
    }

    public void EquipStatAdd(ItemData item)
    {
        //公扁
        if (curEquipWeapon == null && item.valueType == ValueType.Attack)
        {
            for (int i = 0; i < item.stats.Length; i++)
            {
                playerStats.StatAdd("attack", item.stats[i].statsValue);
            }
        }

        //规绢备
        if (curEquipArmor == null && item.valueType == ValueType.Defense)
        {
            for (int i = 0; i < item.stats.Length; i++)
            {
                playerStats.StatAdd("defense", item.stats[i].statsValue);
            }
        }
    }

    public void EquipStatSubtract(ItemData item)
    {
        //公扁
        if (item.valueType == ValueType.Attack)
        {
            for (int i = 0; i < item.stats.Length; i++)
            {
                playerStats.StatSubtract("attack", item.stats[i].statsValue);
            }
        }

        //规绢备
        if (item.valueType == ValueType.Defense)
        {
            for (int i = 0; i < item.stats.Length; i++)
            {
                playerStats.StatSubtract("defense", item.stats[i].statsValue);
            }
        }
    }
}
