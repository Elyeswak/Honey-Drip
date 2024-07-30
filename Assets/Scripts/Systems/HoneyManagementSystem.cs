using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class HoneyManagementSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _honeyText; public static TextMeshProUGUI HoneyText => _instance._honeyText;
    [SerializeField] private Animator _animatorText;
    private static HoneyManagementSystem _instance;
    void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
    }
    // Start is called before the first frame update
    public static bool PlayerPaysHoney(int amount)
    {
        bool hasEnoughHoney = GameManager.Player.SubtractHoney(amount);
        if (!hasEnoughHoney)
        {
            //TODO: Animate Text turn red
            _instance._animatorText.SetTrigger("NotEnoughHoney");
        }
        else
        {
            //TODO: Animate Text turn green
        }
        return hasEnoughHoney;
    }
    public static bool PlayerGainsHoney(int amount) => GameManager.Player.AddHoney(amount);
    public static void IncreasePlayerMaxHoney(InteractableNode node)
    {
        if (!(node.nodeStats is Apiary)) return;
        Apiary nodeStats = (Apiary)node.nodeStats;
        GameManager.Player.IncreaseMaxHoney(nodeStats.honeyMax);
    }
    public static void DecreasePlayerMaxHoney(InteractableNode nodeStats)
    {
        if (!(nodeStats.nodeStats is Apiary)) return;
        Apiary node = (Apiary)nodeStats.nodeStats;
        GameManager.Player.DecreaseMaxHoney(node.honeyMax);
    }
    /// <summary>
    /// Adds honey to the player
    /// </summary>
    /// <param name="amount">How much honey to add</param>
    /// <returns>True if player gained maximum honey, else false</returns>
    public async void HoneyGeneration_ApiaryT1(InteractableNode node)
    {
        NodeStats nodeStats = node.nodeStats;
        //check if nodeStats in an instance of ApiaryStats
        if (!(nodeStats is Apiary)) { await Task.CompletedTask; return; }
        Apiary apiaryStats = (Apiary)nodeStats;
        int timer = 0;
        node.SetProgress(0f);
        while (timer < apiaryStats.harvestingTime && !node.IsProgressCanceled)
        {
            await Task.Delay(1000);
            timer++;
            node.SetProgress((float)timer / apiaryStats.harvestingTime);
            await Task.Yield();
        }
        await Task.CompletedTask;
    }
    public void HoneyHarvesting_ApiaryT1(InteractableNode node)
    {
        NodeStats nodeStats = node.nodeStats;
        //check if nodeStats in an instance of ApiaryStats
        if (!(nodeStats is Apiary)) return;
        Apiary apiaryStats = (Apiary)nodeStats;
        int amount = (int)node.GetProgress() * apiaryStats.honeyAmount;
        PlayerGainsHoney(amount);
        node.SetProgress(0f);
        IInteractableStates interactableState = node;
        interactableState.SwitchToState(InteractableState.Idle);
    }
}