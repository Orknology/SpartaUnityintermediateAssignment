using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public Text quantityText;
    public ItemSlot curSlot;
    public GameObject Equipped;
    private Outline outline;

    public int index;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
        Equipped.SetActive(equipped);
    }

    public void Set(ItemSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = equipped;
            Equipped.SetActive(equipped);
        }
    }
    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory.instance.ItemInfoPanel.SetActive(true);
        Inventory.instance.SelectItem(index);
    }
}
