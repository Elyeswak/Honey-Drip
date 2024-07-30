
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IInteractableStates
{

    virtual public void TriggerOnIdle() { }
    virtual public void TriggerOnProgress() { }
    virtual public void TriggerOnComplete() { }
    public InteractableState interactableState { get; set; }
    virtual public void SwitchNextState()
    {
        switch (interactableState)
        {
            case InteractableState.Idle:
                interactableState = InteractableState.Progress;
                TriggerOnProgress();
                break;
            case InteractableState.Progress:
                interactableState = InteractableState.Complete;
                TriggerOnComplete();
                break;
            case InteractableState.Complete:
                interactableState = InteractableState.Idle;
                TriggerOnIdle();
                break;
            default:
                break;
        }
    }
    virtual public void SwitchToState(InteractableState state)
    {
        interactableState = state;
        switch (state)
        {
            case InteractableState.Idle:
                TriggerOnIdle();
                break;
            case InteractableState.Progress:
                TriggerOnProgress();
                break;
            case InteractableState.Complete:
                TriggerOnComplete();
                break;
        }
    }
}
public abstract class InteractableNode : Node, IInteractableStates
{
    public NodeStats nodeStats;
    public virtual void TriggerOnIdle() { }
    public virtual void TriggerOnProgress() { }
    public virtual void TriggerOnComplete() { }
    public InteractableState interactableState { get => _interactableState; set => _interactableState = value; }
    protected InteractableState _interactableState = InteractableState.Idle;
    protected CancellationTokenSource _idleCancellationTokenSource;
    protected CancellationTokenSource _progressCancellationTokenSource;
    protected CancellationTokenSource _completeCancellationTokenSource;
    public bool IsIdleCanceled => _idleCancellationTokenSource.IsCancellationRequested;
    public bool IsProgressCanceled => _progressCancellationTokenSource.IsCancellationRequested;
    public bool IsCompleteCanceled => _completeCancellationTokenSource.IsCancellationRequested;
    [SerializeField] protected Image _progressIdicator;
    public void SetProgress(float progress) { if (_progressIdicator) _progressIdicator.fillAmount = progress; }
    public float GetProgress() => _progressIdicator.fillAmount;
    void Awake()
    {
        _idleCancellationTokenSource = new CancellationTokenSource();
        _progressCancellationTokenSource = new CancellationTokenSource();
        _completeCancellationTokenSource = new CancellationTokenSource();
    }
    public void NewIdle() => _idleCancellationTokenSource = new CancellationTokenSource();
    public void NewProgress() => _progressCancellationTokenSource = new CancellationTokenSource();
    public void NewComplete() => _completeCancellationTokenSource = new CancellationTokenSource();
    public void CancelIdle() => _idleCancellationTokenSource.Cancel();
    public void CancelProgress() => _progressCancellationTokenSource.Cancel();
    public void CancelComplete() => _completeCancellationTokenSource.Cancel();
    public void CancelAll()
    {
        _idleCancellationTokenSource?.Cancel();
        _progressCancellationTokenSource?.Cancel();
        _completeCancellationTokenSource?.Cancel();
    }
    public void DisposeAll()
    {
        _idleCancellationTokenSource?.Dispose();
        _progressCancellationTokenSource?.Dispose();
        _completeCancellationTokenSource?.Dispose();
    }
    void OnDisable() => CancelAll();
    void OnDestroy() => DisposeAll();
}