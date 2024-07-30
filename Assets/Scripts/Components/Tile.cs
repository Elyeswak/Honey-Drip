using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    private Node _occupiedNode = null; public Node OccupiedNode => _occupiedNode;
    [SerializeField] private UnityEvent _TileInteraction;
    public void SetOccupiedBy(Node node) => _occupiedNode = node;
    public bool isOccupied => _occupiedNode != null;
}
