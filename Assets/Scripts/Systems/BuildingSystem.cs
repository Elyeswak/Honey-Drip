using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
public class BuildingSystem : MonoBehaviour
{
    private static BuildingSystem _instance;
    [SerializeField] private Building _apiaryPrefab;
    public static Building Apiary => _instance._apiaryPrefab;
    [SerializeField] private Building _shopPrefab;
    public static Building Shop => _instance._shopPrefab;
    void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
    }
    public static Building CreateBuilding(Node node, Building buildingPrefab = null)
    {
        if (buildingPrefab == null) buildingPrefab = _instance._apiaryPrefab;
        Building building = Instantiate(buildingPrefab, node.hex.ToPlanar(), Quaternion.identity);
        building.Place(node.hex);
        Tile tile = node.GetComponent<Tile>();
        tile.SetOccupiedBy(building);
        return building;
    }

    public void BuildApiary(Apiary apiary)
    {
        Node node = PlayerController.highlightedNode;
        Tile tile = node.GetComponent<Tile>();
        if (tile.isOccupied) return;
        if (HoneyManagementSystem.PlayerPaysHoney(apiary.cost))
        {
            CreateBuilding(node, apiary._Prefab);
            UIManager.ToggleWorldPanel(false);
        }
        else
            print("Not enough honey!");
    }
}
