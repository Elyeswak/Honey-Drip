using System;
using Unity.AI.Navigation;
using UnityEngine;
using System.Collections.Generic;

public class GridMapGenerator : MonoBehaviour
{
    private static GridMapGenerator _instance;
    [Header("Prefabs")]
    [SerializeField] private Node _hexNodePrefab;
    //A 2D list of nodes
    private List<List<Node>> _gridMap;
    [Header("Variation")]
    [SerializeField][Range(1, 100)] private int _radius = 10;
    private int _initialRadius;
    [SerializeField][Range(0, 0.1f)] private float _heightVariation = 10;
    [Header("Debug")]
    [SerializeField] private bool _labeledNodes = false;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    public static Node GetNode(Vector2 position) => _instance._gridMap[_instance._radius +(int)position.x][_instance._radius +(int)position.y];

    public void GenerateNodes()
    {
        _gridMap = new List<List<Node>>();

        for (int i = 0; i < _radius * 2 + 1; i++)
        {
            _gridMap.Add(new List<Node>());
            for (int j = 0; j < _radius * 2 + 1; j++)
            {
                _gridMap[i].Add(null); // Initialize each element to null
            }
        }

        Hex centerHex = Hex.zero;
        foreach (Hex hex in Hex.Spiral(centerHex, 0, _radius))
        {
            int x = hex.q + _radius;
            int y = hex.r + _radius;
            Node newNode = Instantiate(_hexNodePrefab, hex.ToWorld(), Quaternion.identity, transform);
            newNode.hex = hex;
            newNode.ApplyTransform();
            newNode.isLabeled = _labeledNodes;

            // Slightly raise the Y position
            if (_heightVariation != 0)
            {
                float heightVariation = UnityEngine.Random.Range(-_heightVariation, _heightVariation);
                newNode.transform.localPosition += new Vector3(0, heightVariation, 0);
            }

            _gridMap[x][y] = newNode;
        }
    }
    void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        _instance = this;
    }

    public static void Init()
    {
        _instance._initialRadius = _instance._radius;
        _instance.GenerateNodes();
        _instance._navMeshSurface.BuildNavMesh();
        //GameManager.CreatePlayer(transform.position);

        //
    }

    void OnValidate()
    {
        if (_gridMap == null) return;
        if (!Application.isPlaying) return;
        _VaryHeight();
    }

    private void _VaryHeight()
    {
        Hex centerHex = Hex.zero;
        foreach (Hex hex in Hex.Spiral(centerHex, 0, _initialRadius))
        {
            int x = hex.q + _initialRadius;
            int y = hex.r + _initialRadius;
            if (_gridMap[x][y] != null)
            {
                _gridMap[x][y].isLabeled = _labeledNodes;
                if (_heightVariation != 0)
                {
                    float heightVariation = UnityEngine.Random.Range(-_heightVariation, _heightVariation);
                    _gridMap[x][y].transform.localPosition = new Vector3(_gridMap[x][y].transform.localPosition.x, heightVariation, _gridMap[x][y].transform.localPosition.z);
                }
            }
        }
    }
}