using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    [Header("Selected Item")]

    private ItemSlot selectedItem;
    private ItemSlot previousWeapon;
    private ItemSlot previousArmor;

    private int selectedItemIndex;
    public Image selectedItemIcon;
    public Text selectedItemName;
    public Text selectedItemDescription;
    public Text selectedItemStatNames;
    public Text selectedItemStatValues;

    public GameObject ItemInfoPanel;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;

    private int curEquipWeaponIndex = -1;
    private int curEquipArmorIndex = -1;

    private PlayerStats playerStats;
    private StatUI statUI;

    public List<ItemData> beginningItems;

    private void Awake()
    {
        instance = this;
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

        for (int i = 0; i < beginningItems.Count; i++)
        {
            AddItem(beginningItems[i]);
        }

        ClearSeletecItemWindow();
    }
    public void AddItem(ItemData item)
    {
        if (item.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }
    ItemSlot GetItemStack(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        selectedItemIcon.sprite = selectedItem.item.icon;

        foreach (ValueType valueType in Enum.GetValues(typeof(ValueType)))
        {
            selectedItemStatNames.text = selectedItem.item.valueType.ToString() + "\n";
        }

        for (int i = 0; i < selectedItem.item.stats.Length; i++)
        {
            selectedItemStatValues.text += selectedItem.item.stats[i].statsValue.ToString() + "\n";
        }

        for (int i = 0; i < selectedItem.item.consumables.Length; i++)
        {
            selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive((selectedItem.item.type == ItemType.Armor || selectedItem.item.type == ItemType.Weapon) && !uiSlots[index].equipped);
        unEquipButton.SetActive((selectedItem.item.type == ItemType.Armor  || selectedItem.item.type == ItemType.Weapon) && uiSlots[index].equipped);
    }

    public void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        ItemInfoPanel.SetActive(false);
        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
    }

    public void OnUseButton()
    {
        if (selectedItem.item.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                playerStats.Heal(selectedItem.item.consumables[i].value);
            }
        }
        RemoveSelectedItem();
    }

    public void OnEquipButton()
    {
        if (curEquipWeaponIndex >= 0 && selectedItem.item.type == ItemType.Weapon && uiSlots[curEquipWeaponIndex].equipped)
        {
            UnEquip(curEquipWeaponIndex);
        }
        else if (curEquipArmorIndex >= 0 && selectedItem.item.type == ItemType.Armor && uiSlots[curEquipArmorIndex].equipped)
        {
            UnEquip(curEquipArmorIndex);
        }
            

        uiSlots[selectedItemIndex].equipped = true;

        if (selectedItem.item.type == ItemType.Weapon)
        {
            curEquipWeaponIndex = selectedItemIndex;
            previousWeapon = selectedItem;
        }
            
        else if (selectedItem.item.type == ItemType.Armor)
        {
            curEquipArmorIndex = selectedItemIndex;
            previousArmor = selectedItem;
        }
            
        EquipManager.instance.EquipNew(selectedItem.item);
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        uiSlots[index].equipped = false;
        if(selectedItem.item.type == ItemType.Weapon)
        {
            EquipManager.instance.EquipStatSubtract(previousWeapon.item);
            EquipManager.instance.UnEquipWeapon();
        }
        else if (selectedItem.item.type == ItemType.Armor)
        {
            EquipManager.instance.EquipStatSubtract(previousArmor.item);
            EquipManager.instance.UnEquipArmor();
        }
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(index);
        }
    }
    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }

    private void RemoveSelectedItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            if (uiSlots[selectedItemIndex].equipped)
            {
                UnEquip(selectedItemIndex);
            }

            selectedItem.item = null;
            ClearSeletecItemWindow();
        }

        UpdateUI();
    }
}
