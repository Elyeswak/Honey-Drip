using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Material _highlightMat;
    [SerializeField] private Material _focusedMat;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _agent;
    [Header("Movement")]
    [SerializeField] private LayerMask _clickableLayers;
    public PInventory inventory;
    private Renderer _highlightedNodeRenderer = null;
    public static Node highlightedNode = null;
    GameObject GameCanvas;
    //[SerializeField] private Item[] items;

    private bool isInputEnabled = true;

    private void Awake()
    {
        inventory = new PInventory();
    }

    private void Start()
    {
        GameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
 /*       foreach (Item item in items)
        {
            Item it = ScriptableObject.Instantiate<Item>(item);
            inventory.AddItem(it);

        }*/
    }

    public void DisableInput()
    {
        isInputEnabled = false;
        Debug.Log("Player input disabled");
    }

    public void EnableInput()
    {
        isInputEnabled = true;
        Debug.Log("Player input enabled");
    }

    public void _OnCursorMovement(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        if (context.performed)
        {
            Vector2 mousePosition = context.ReadValue<Vector2>();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {

                string layer = LayerMask.LayerToName(hit.transform.gameObject.layer);
                switch (layer)
                {
                    case "HexTile":
                        if (WorldPanel.isFocused) break;
                        Renderer renderer = hit.transform.GetComponent<Renderer>();
                        Material material = renderer.material;
                        if (_highlightedNodeRenderer != null)
                        {
                            _highlightedNodeRenderer.material = material;
                            highlightedNode.isSelected = false;
                        }
                        hit.transform.GetComponent<Renderer>().material = _highlightMat;
                        _highlightedNodeRenderer = renderer;
                        highlightedNode = hit.transform.GetComponentInParent<Node>();
                        highlightedNode.isSelected = true;
                        break;
                }
            }
        }
    }

    public PInventory GetInventory()
    {
        return this.inventory;
    }

    public bool CheckIfItem(Item item)
    {
        bool check = false;
        var items = inventory.GetItemList();
        foreach (Item i in items)
        {
            if (i.itemName == item.itemName)
            {
                check = true;
                return check;
            }
        }
        return check;
    }

    public void _OnSecondary(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        if (!context.performed) return;
        UIManager.ToggleWorldPanel(false);
        RaycastHit hit;
        Vector3 mousePos = GameManager.Mouse.position.ReadValue();
        if (Physics.Raycast(GameManager.Camera.ScreenPointToRay(mousePos), out hit, 100, _clickableLayers))
        {
            PlayerCamera.FollowTargetAsync(GameManager.PlayerController.transform).GetAwaiter();
            _agent.destination = hit.point;
        }
    }

    public void _OnPrimary(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        if (Application.isFocused == false) return;
        if (WorldPanel.isFocused) return;
        if (!context.performed) return;
        Tile tile = highlightedNode.GetComponent<Tile>();
        _highlightedNodeRenderer.material = _focusedMat;
        if (tile.isOccupied)
        {
            IInteractableStates interactable = tile.OccupiedNode.GetComponent<IInteractableStates>();
            interactable.SwitchNextState();
            return;
        }
        UIManager.SetWorldPanel();
    }

    public void _OnZoom(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        if (!context.performed) return;
        if (context.ReadValue<float>() > 0)
            PlayerCamera.ZoomInAsync().GetAwaiter();
        else
            PlayerCamera.ZoomOutAsync().GetAwaiter();
    }
}
