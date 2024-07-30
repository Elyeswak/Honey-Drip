using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PInventory : MonoBehaviour
{
    public event EventHandler OnitemlistChanged;
    private List<Item> itemsList;
    public PlayerController playerScript;
    private GameObject Player;

    public PInventory()
    {
        itemsList = new List<Item>();
        Debug.Log("inventory created !!");
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<PlayerController>();
    }

    public void AddItem(Item item)
    {
        if (playerScript.CheckIfItem(item))
        {
            foreach (Item it in itemsList)
            {
                if (it.itemName == item.itemName)
                {
                    int newQuantity = it.Quantity + item.Quantity;
                    if (newQuantity <= it.maxStack)
                    {
                        it.Quantity += item.Quantity;
                    }
                    else
                    {
                        int excessQuantity = newQuantity - it.maxStack;
                        it.Quantity = it.maxStack;
                        Debug.Log($"Added {item.itemName} to max stack size {it.maxStack}. Excess quantity of {excessQuantity} not added.");
                    }
                    OnitemlistChanged?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        else
        {
            if (item.Quantity <= item.maxStack)
            {
                itemsList.Add(item);
                GameManager.InventoryManager.AddItemtoUI(item);
                OnitemlistChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                int excessQuantity = item.Quantity - item.maxStack;
                item.Quantity = item.maxStack;
                itemsList.Add(item);
                GameManager.InventoryManager.AddItemtoUI(item);
                Debug.Log($"Added {item.itemName} to max stack size {item.maxStack}. Excess quantity of {excessQuantity} not added.");
                OnitemlistChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public List<Item> GetItemList()
    {
        return itemsList;
    }

    public void RemoveItem(Item item)
    {
        if (item == null) return;

        foreach (Item i in itemsList.ToArray())
        {
            if (i.itemName == item.itemName)
            {
                i.Quantity--;
                if (i.Quantity <= 0)
                {
                    itemsList.Remove(i);
                    Debug.Log($"Item {i.itemName} removed from inventory because quantity is zero.");
                }
                break;
            }
        }

        OnitemlistChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItemCompletely(Item item)
    {
        if (item == null) return;

        itemsList.Remove(item);
        Debug.Log($"Item {item.itemName} completely removed from inventory.");
        OnitemlistChanged?.Invoke(this, EventArgs.Empty);
    }
}
