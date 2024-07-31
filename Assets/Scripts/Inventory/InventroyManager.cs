using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventroyManager : MonoBehaviour
{
    public static InventroyManager Instance { get; private set; }

    public GameObject inventory;
    public GameObject cardCollection;
    public Transform inventorySlotHolder;
    public List<bool> isfull;
    public List<Transform> slots;
    public int currentSlot;
    public Transform cursor;
    public Vector3 offset;
    public GameObject ItemPrefab;
    public GameObject popup; // Reference to the drop popup UI
    public static PlayerController playerScript;
    private item_ui itemUiScript;

    private Vector2 mousePosition;
    private InputActions playerInputActions;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (inventory != null)
        {
            inventory.SetActive(false);
        }

        if (cardCollection != null)
        {
            cardCollection.SetActive(false);
        }
    }

    private void Start()
    {
        InitInventory();
        SetSlotIds();
        checkSlots();
        itemUiScript = ItemPrefab.GetComponent<item_ui>();
        playerInputActions = new InputActions();
        playerInputActions.UI.Point.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();
        playerInputActions.Enable();
        SFXManager.PlayMusic("Main");
    }

    private void Update()
    {
        if (inventory.activeSelf)
        {
            cursor.position = (Vector3)mousePosition + offset;
        }

        cursor.gameObject.SetActive(cursor.childCount > 0);

    }

    void InitInventory()
    {
        for (int i = 0; i < inventorySlotHolder.childCount; i++)
        {
            slots.Add(inventorySlotHolder.GetChild(i));
            isfull.Add(false);
        }
    }

    public void checkSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            isfull[i] = slots[i].childCount > 0;
        }
    }

    public void AddItemtoUI(Item item)
    {
        if (checkItemInInventory(item))
        {
            var slotholder = inventory.transform.GetChild(1);

            foreach (Transform child in slotholder)
            {
                if (child.GetComponent<item_ui>().StoredItem.itemName == item.itemName)
                {
                    Debug.Log("item quantity incremented");
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (!isfull[i])
                {
                    itemUiScript.AssignItemtoIUi(item);
                    Instantiate(ItemPrefab, slots[i]);
                    Debug.Log("item added to Ui");
                    checkSlots();
                    return;
                }
            }
        }
    }

    void SetSlotIds()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var slotComponent = slots[i].GetComponent<sloot>();
            if (slotComponent != null)
            {
                slotComponent.ID = i;
            }
        }
    }

    public void OpenInventory()
    {
        bool isActive = !inventory.activeSelf;
        inventory.SetActive(isActive);
    }

    bool checkItemInInventory(Item item)
    {
        inventory.SetActive(true);
        var slotholder = inventory.transform.GetChild(1);

        foreach (Transform child in slotholder)
        {
            if (child.childCount > 0)
            {
                if (child.GetChild(0).GetComponent<item_ui>().StoredItem.itemName == item.itemName)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        inventory.SetActive(true);
        var slotholder = inventory.transform.GetChild(1);
        if (checkItemInInventory(item))
        {
            foreach (Transform child in slotholder)
            {
                if (child.childCount > 0)
                {
                    if (child.GetChild(0).GetComponent<item_ui>().StoredItem.itemName == item.itemName)
                    {
                        if (child.GetChild(0).GetComponent<item_ui>().StoredItem.Quantity <= 1)
                        {
                            Destroy(child.GetChild(0).gameObject);
                        }
                    }
                }
            }
        }
    }

    // New function to completely remove an item
    public void RemoveItemCompletely(Item item)
    {

        playerScript.inventory.RemoveItemCompletely(item);

        playerScript.inventory.ItemList();
        checkSlots();
    }

    public void HaltPlayerInput()
    {
        if (playerScript != null)
        {
            playerScript.DisableInput();
        }
    }

    public void ResumePlayerInput()
    {
        if (playerScript != null)
        {
            playerScript.EnableInput();
        }
    }

    // Save and Load Inventory
    public void SaveInventory()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        PInventoryData data = playerScript.inventory.GetInventoryData();
        SaveLoadUtility.SaveData(data, filePath);
        Debug.Log("Inventory saved");
    }

    public void LoadInventory()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        PInventoryData loadedData = SaveLoadUtility.LoadData<PInventoryData>(filePath);
        if (loadedData != null)
        {
            playerScript.inventory.SetInventoryData(loadedData);
            foreach (Item item in playerScript.inventory.GetItemList())
            {
                AddItemtoUI(item);
            }
            Debug.Log("Inventory loaded");
        }
        else
        {
            Debug.LogWarning("No inventory data found");
        }
    }
}