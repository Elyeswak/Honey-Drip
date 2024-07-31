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
        manager.HaltPlayerInput();

        if (CurrentSlot.transform.childCount == 0)
        {
            Debug.Log("UseStoredItem: CurrentSlot is empty");
            manager.ResumePlayerInput();
            return;
        }

        var child = CurrentSlot.transform.GetChild(0);
        var itemscript = child.GetComponent<item_ui>();

        if (itemscript != null)
        {
            itemscript.UseItemEffect();
            Debug.Log($"UseStoredItem: Used item {itemscript.StoredItem.itemName}");

            if (itemscript.StoredItem.Quantity <= 0)
            {
                GameManager.PlayerController.inventory.RemoveItemCompletely(itemscript.StoredItem);
                ClearTooltip();
                Destroy(child.gameObject);
                Debug.Log($"UseStoredItem: Removed item {itemscript.StoredItem.itemName} because quantity is zero");
            }
        }
        else
        {
            Debug.LogWarning("UseStoredItem: item_ui component not found on child");
        }

        manager.ResumePlayerInput();
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
            GameManager.PlayerController.inventory.RemoveItemCompletely(item);
            ClearTooltip();
            Destroy(child.gameObject);
            Debug.Log($"DeleteStoredItem: Deleted item {item.itemName}");
        }
        else
        {
            Debug.LogWarning("DeleteStoredItem: item_ui component not found on child");
        }

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
                ClearTooltip();
            }
        }
        else
        {
            ClearTooltip();
        }
    }

    public void ClearTooltip()
    {
        var tooltipTrigger = CurrentSlot.GetComponent<TooltipTrigger>();
        if (tooltipTrigger != null)
        {
            tooltipTrigger.Clear();
            Debug.Log("ClearTooltip: Tooltip cleared.");
        }
        else
        {
            Debug.Log("ClearTooltip: TooltipTrigger component not found.");
        }
    }

    public void OnSlotContentChanged()
    {
        UpdateTooltip();
    }
}
