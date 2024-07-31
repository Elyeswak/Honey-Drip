using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class item_ui : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image imageIcon;
    public Item StoredItem;
    InventroyManager invManager;
    public TextMeshProUGUI QuantityTxt;

    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject popup;

    private void Awake()
    {
        invManager = GameObject.FindGameObjectWithTag("INmanager").GetComponent<InventroyManager>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        popup = invManager.popup;
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        imageIcon.sprite = StoredItem.icon;
        QuantityTxt.text = StoredItem.Quantity.ToString();
        if (transform.parent != null && transform.parent.GetComponent<sloot>() != null)
        {
            transform.parent.GetComponent<sloot>().OnSlotContentChanged();
        }
    }

    public void AssignItemtoUi(Sprite _imageIcon, int _quantity, Item itemStored)
    {
        imageIcon.sprite = _imageIcon;
        this.StoredItem = itemStored;
        QuantityTxt.text = _quantity.ToString();

        if (transform.parent != null && transform.parent.GetComponent<sloot>() != null)
        {
            transform.parent.GetComponent<sloot>().OnSlotContentChanged();
        }
    }

    public void AssignItemtoIUi(Item item)
    {
        StoredItem = item;
        StoredItem.init();

        if (transform.parent != null && transform.parent.GetComponent<sloot>() != null)
        {
            transform.parent.GetComponent<sloot>().OnSlotContentChanged();
        }
    }

    public void UseItemEffect()
    {
        if (StoredItem == null) return;

        StoredItem.ItemEffect();
        invManager.RemoveItem(StoredItem);
        InventroyManager.playerScript.inventory.RemoveItem(StoredItem);

        // Notify the slot about the content change
        if (transform.parent != null && transform.parent.GetComponent<sloot>() != null)
        {
            transform.parent.GetComponent<sloot>().OnSlotContentChanged();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;

        // Clear the tooltip of the original slot
        if (originalParent != null && originalParent.GetComponent<sloot>() != null)
        {
            originalParent.GetComponent<sloot>().ClearTooltip();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform newParent = GetNewParent();

        if (newParent != null && newParent.childCount == 0)
        {
            transform.SetParent(newParent);
            transform.localPosition = Vector3.zero;
        }
        // Item dropped outside the inventory or slot
        else if (newParent == null)
        {
            StartCoroutine(ShowDropPopup());
        }
        else
        {
            transform.SetParent(originalParent);
            // Reset the position in the original slot
            transform.localPosition = Vector3.zero;
        }

        canvasGroup.blocksRaycasts = true;
        invManager.checkSlots();

        if (transform.parent != null && transform.parent.GetComponent<sloot>() != null)
        {
            transform.parent.GetComponent<sloot>().OnSlotContentChanged();
        }
    }

    private Transform GetNewParent()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Slot"))
            {
                return result.gameObject.transform;
            }
        }

        return null;
    }

    private IEnumerator ShowDropPopup()
    {
        popup.SetActive(true);

        bool userResponse = false;
        bool hasResponded = false;


        void OnYesButtonClicked()
        {
            userResponse = true;
            hasResponded = true;
        }

        void OnNoButtonClicked()
        {
            userResponse = false;
            hasResponded = true;
        }


        popup.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(OnYesButtonClicked);
        popup.transform.Find("NoButton").GetComponent<Button>().onClick.AddListener(OnNoButtonClicked);


        yield return new WaitUntil(() => hasResponded);


        popup.transform.Find("YesButton").GetComponent<Button>().onClick.RemoveListener(OnYesButtonClicked);
        popup.transform.Find("NoButton").GetComponent<Button>().onClick.RemoveListener(OnNoButtonClicked);

        popup.SetActive(false);

        if (userResponse)
        {
            invManager.RemoveItemCompletely(StoredItem);
            Destroy(gameObject);
        }
        else
        {
            // Reset the position in the original slot
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }

        invManager.checkSlots();

        if (transform.parent != null && transform.parent.GetComponent<sloot>() != null)
        {
            transform.parent.GetComponent<sloot>().OnSlotContentChanged();
        }
    }
}
