using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 
/// TODO: Add description
/// </summary>
[CreateAssetMenu(fileName = "InteractablePreset", menuName = "ScriptableObjects/InteractablePreset")]
public class InteractablePreset : ScriptableObject
{
    [Header("Events")]
    public UnityEvent<IInteractableStates> OnIdle;
    public UnityEvent<IInteractableStates> OnProgress;
    public UnityEvent<IInteractableStates> OnComplete;
}
/// <summary>
/// Building State, Idle, Progress, Complete
/// </summary>
public enum InteractableState
{
    None,
    Idle,
    Progress,
    Complete
}