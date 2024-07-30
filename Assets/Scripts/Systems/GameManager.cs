using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static Camera _camera;
    public static Camera Camera => _camera;
    private static Mouse _mouse;
    public static Mouse Mouse => _mouse;
    private PlayerController _playerController;
    public static PlayerController PlayerController => _instance._playerController;
    private Player _player;
    public static Player Player => _instance._player;

    [Header("References")]
    [SerializeField] private PlayerController _playerPrefab;

    private InventroyManager _inventoryManager;
    public static InventroyManager InventoryManager => _instance._inventoryManager;

    public static void CreatePlayer(Vector3 position)
    {
        _instance._playerController = Instantiate(_instance._playerPrefab, position, Quaternion.identity);
        PlayerCamera.FollowTargetAsync(_instance._playerController.transform).GetAwaiter();
        _instance._player = _instance._playerController.GetComponent<Player>();
    }

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _camera = Camera.main;
        _mouse = Mouse.current;
        GridMapGenerator.Init();
        CreatePlayer(transform.position);

        _inventoryManager = FindObjectOfType<InventroyManager>();
        if (_inventoryManager != null)
        {
            InventroyManager.playerScript = Player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("InventoryManager not found in the scene!");
        }

        Node node = GridMapGenerator.GetNode(new Vector2(3, 3));
        BuildingSystem.CreateBuilding(node, BuildingSystem.Shop);
    }

    public void Print_UT(string text)
    {
        Debug.Log("From Game Manager: " + text);
    }

    public void Quit() => Application.Quit();
}
