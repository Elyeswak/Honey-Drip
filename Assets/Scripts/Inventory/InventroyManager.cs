using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    item_ui itemUiScript;
    public static PlayerController playerScript;

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
        itemUiScript = ItemPrefab.gameObject.GetComponent<item_ui>();

        playerInputActions = new InputActions();
        playerInputActions.UI.Point.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();
        playerInputActions.Enable();
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
                    Debug.Log("ui item exists!");
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
                            Debug.Log("item deleted");
                            Destroy(child.GetChild(0).gameObject);
                        }
                    }
                }
            }
        }
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
}
