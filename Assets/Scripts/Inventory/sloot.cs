using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sloot : MonoBehaviour
{
    public GameObject CurrentSlot;
    public int ID;
    public InventroyManager manager;
    public ReadPlayerInput DeleteUiscript;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("INmanager").GetComponent<InventroyManager>();
        Debug.Log($"sloot Start: manager assigned, CurrentSlot={CurrentSlot}");
        UpdateTooltip();
    }

    public void setId()
    {
        manager.currentSlot = ID;
        Debug.Log($"setId: currentSlot set to {ID}");
    }

    public void UseStoredItem()
    {
        if (CurrentSlot.transform.childCount == 0)
        {
            Debug.Log("UseStoredItem: CurrentSlot is empty");
            return;
        }

        var child = CurrentSlot.transform.GetChild(0);
        var itemscript = child.GetComponent<item_ui>();

        if (itemscript != null)
        {
            itemscript.UseItemEffect();
            Debug.Log($"UseStoredItem: Used item {itemscript.StoredItem.itemName}");
        }
        else
        {
            Debug.LogWarning("UseStoredItem: item_ui component not found on child");
        }

        UpdateTooltip();
    }

    public Item DeleteStoredItem()
    {
        var item = new Item();
        if (CurrentSlot.transform.childCount == 0)
        {
            Debug.Log("DeleteStoredItem: CurrentSlot is empty");
            return item;
        }

        var child = CurrentSlot.transform.GetChild(0);
        var itemscript = child.GetComponent<item_ui>();

        if (itemscript != null)
        {
            item = itemscript.StoredItem;
            Destroy(child.gameObject);
            Debug.Log($"DeleteStoredItem: Deleted item {item.itemName}");
        }
        else
        {
            Debug.LogWarning("DeleteStoredItem: item_ui component not found on child");
        }

        UpdateTooltip();
        return item;
    }

    public void OnMiddleClick()
    {
        DeleteUiscript.Slot = CurrentSlot;
        Debug.Log($"OnMiddleClick: Slot ID {DeleteUiscript.Slot.GetComponent<sloot>().ID}");
        DeleteUiscript.ShowDeleteItemUI();
    }

    public void UpdateTooltip()
    {
        var tooltipTrigger = CurrentSlot.GetComponent<TooltipTrigger>();
        if (tooltipTrigger == null)
        {
            Debug.LogWarning("UpdateTooltip: TooltipTrigger component not found on CurrentSlot.");
            return;
        }

        if (CurrentSlot.transform.childCount > 0)
        {
            var itemUi = CurrentSlot.transform.GetChild(0).GetComponent<item_ui>();
            if (itemUi != null)
            {
                tooltipTrigger.header = itemUi.StoredItem.itemName;
                tooltipTrigger.content = itemUi.StoredItem.description; 
                Debug.Log($"UpdateTooltip: Tooltip updated - Header: {tooltipTrigger.header}, Content: {tooltipTrigger.content}");
            }
            else
            {
                Debug.LogWarning("UpdateTooltip: item_ui component not found on child of CurrentSlot.");
            }
        }
        else
        {
            tooltipTrigger.header = "";
            tooltipTrigger.content = "";
            Debug.Log("UpdateTooltip: Tooltip cleared.");
        }
    }

    public void ClearTooltip()
    {
        var tooltipTrigger = CurrentSlot.GetComponent<TooltipTrigger>();
        if (tooltipTrigger != null)
        {
            tooltipTrigger.header = "";
            tooltipTrigger.content = "";
            Debug.Log("ClearTooltip: Tooltip cleared.");
        }
    }

    // Call this method whenever the slot content changes
    public void OnSlotContentChanged()
    {
        UpdateTooltip();
    }
}
