using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField] private WorldPanel _worldPanel;
    public static WorldPanel WorldPanel => _instance._worldPanel;
    void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
        _worldPanel.gameObject.SetActive(false);

    }
    public async static void SetWorldPanel()
    {
        await _instance._worldPanel.HaltInput();
        ToggleWorldPanel(true);
    }
    public static void ToggleWorldPanel(bool state) => _instance._worldPanel.gameObject.SetActive(state);
    public static void SetWorldPanelEvent(UnityEvent newEvent) => _instance._worldPanel.SetCurrentEvent(newEvent);
}
