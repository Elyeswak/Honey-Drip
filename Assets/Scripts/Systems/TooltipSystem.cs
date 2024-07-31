using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem _instance;
    [SerializeField] private Tooltip tooltip;

    private void Awake()
    {
        _instance = this;
    }
     public static void Show(string description, string header ="")
    {
        _instance.tooltip.SetText(description, header);
        _instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        _instance.tooltip.gameObject.SetActive(false);
    }

}
