using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorldPanel : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite;
    public static bool isFocused => UIManager.WorldPanel.gameObject.activeSelf;
    [SerializeField] private UnityEvent _defaultEvent;
    private UnityEvent _currentBuildingUnityEvent; public void SetCurrentEvent(UnityEvent newEvent) => _currentBuildingUnityEvent = newEvent;
    private static WorldPanel _instance;
    void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        _instance = this;
    }

    async void OnEnable()
    {
        await HaltInput();
    }

    public async Task HaltInput(int amount = 10)
    {
        await Task.Delay(amount);
    }

    void OnDisable()
    {
    }
}
