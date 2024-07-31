using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadPlayerInput : MonoBehaviour
{
    public GameObject DeleteItemUi;
    public Item ItemToDelete;
    public string inputString;
    public TMP_InputField inputfield;
    private PInventory playerInv;
    public GameObject Slot;

    public GameObject InputfiledGm;

    // Start is called before the first frame update
    private void Awake()
    {
        HideDeleteItemUI();
        inputfield = InputfiledGm.GetComponent<TMP_InputField>();
        Slot = this.gameObject;


    }
  
    public void ReadStringInput(string s)
    {
        inputString = s;
        Debug.Log(inputString.ToString());
    }

    public void ShowDeleteItemUI()
    {
        DeleteItemUi.SetActive(true);

    }

    public void HideDeleteItemUI()
    {
        DeleteItemUi.SetActive(false);

    }
    public void ConfirmDelete()
    {
        if (inputfield.text == "delete" || inputfield.text == "Delete")
        {
            Slot.GetComponent<sloot>().DeleteStoredItem();
            DeleteItemUi.SetActive(false);
        }

        Debug.Log(inputfield.text);

    }
    public void GetItemToDelete(Item it)
    {
        ItemToDelete = it;
    }
}
