using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WorldPanelAction : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _priceIndicator;
    [SerializeField] private NodeStats nodeStats;
    
    void Awake()
    {
        _priceIndicator.text = nodeStats.cost.ToString();
    }
}
