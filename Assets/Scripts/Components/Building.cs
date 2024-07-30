using UnityEngine;
using UnityEngine.Events;

public class Building : InteractableNode
{
    [Header("References")]
    [SerializeField] private InteractablePreset _interactablePreset;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private UnityEvent _startEvent;
    [SerializeField] private UnityEvent _endEvent;
    [SerializeField] private UnityEvent _localEventOnIdle;
    [SerializeField] private UnityEvent _localEventOnProgress;
    [SerializeField] private UnityEvent _localEventOnComplete;
    override public void TriggerOnIdle()
    {
        if (!IsCompleteCanceled) CancelComplete();
        NewIdle();
        _interactablePreset.OnIdle?.Invoke(this);
        _localEventOnIdle?.Invoke();
    }
    override public void TriggerOnProgress()
    {
        if (!IsIdleCanceled) CancelIdle();
        NewProgress();
        _interactablePreset.OnProgress?.Invoke(this);
        _localEventOnProgress?.Invoke();
        _progressIdicator.GetComponent<Animator>().SetBool("inProgress", true);

    }
    override public void TriggerOnComplete()
    {
        if (!IsProgressCanceled) CancelProgress();
        NewComplete();
        _interactablePreset.OnComplete?.Invoke(this);
        _localEventOnComplete?.Invoke();
        _progressIdicator.GetComponent<Animator>().SetBool("inProgress", false);
    }
    public void TogglePlay() => _particleSystem.TogglePlay();
    public void Place(Hex hex)
    {
        transform.position = hex.ToWorld();
        ApplyTransform();
    }
    void Start()
    {
        _startEvent?.Invoke();
    }
    void OnDestroy()
    {
        _endEvent?.Invoke();
    }
}