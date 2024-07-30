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

    public  void  AddItem(Item item)
    {
        if (playerScript.CheckIfItem(item))
        {
    
            foreach (Item it in itemsList)
            {
                if (it.itemName == item.itemName)
                {
                    it.Quantity += item.Quantity;

                    return;
                }
               


            }

        }

        else
        {
            itemsList.Add(item);
            OnitemlistChanged?.Invoke(this, EventArgs.Empty);
            
        }


      

    }
     
    public List<Item> GetItemList()
    {

        return itemsList;
    }

    public void RemoveItem(Item item)
    {
        if (item == null) return;
        foreach(Item i in GetItemList().ToArray())
        {
            if(i.itemName == item.itemName)
            {
                if(i.Quantity > 1)
                {
                    i.Quantity--;
                    Debug.Log("dkhal ll remove");
                }else
                {
                    itemsList.Remove(i);
                    Debug.Log("dkhal ll remove 2");
                }

            }
        }


    }


}
