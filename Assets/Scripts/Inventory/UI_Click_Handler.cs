using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Click_Handler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onLeftClick;
    public UnityEvent onRightClick;
    public UnityEvent onMiddleClick;

    private sloot parentSloot;

    public void OnPointerClick(PointerEventData eventData)
    {
        
            parentSloot = GetComponentInParent<sloot>();
            if (parentSloot == null)
            {
                Debug.LogError("UI_Click_Handler: Parent sloot script not found.");
                return;
            }
        

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onLeftClick.Invoke();
            parentSloot.UseStoredItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick.Invoke();
           
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            onMiddleClick.Invoke();
            parentSloot.OnMiddleClick();
        }
    }
}
