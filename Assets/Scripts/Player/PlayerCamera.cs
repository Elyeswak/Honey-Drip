using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _target; public void SetTarget(Transform target) => _target = target;
    [SerializeField] private float _smoothSpeed = 8f;
    [SerializeField] private float _zoomSpeed = 8f;
    private Vector3 _offset;
    [Header("Settings")]
    [SerializeField] private int _currentZoomProfile = 0;
    [SerializeField] private ZoomProfiles _zoomProfiles;
    public static Vector3 CurrentZoomProfile => _instance._zoomProfiles.zooms[_instance._currentZoomProfile];
    private static Task _followTask; private CancellationTokenSource _followingCancellationTokenSource;
    private static Task _zoomTask; private CancellationTokenSource _zoomCancellationTokenSource;
    private static PlayerCamera _instance;

    private void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        _instance = this;

        _offset = _zoomProfiles.zooms[_currentZoomProfile];
    }
    private static async Task FollowTarget(Transform target = null)
    {
        if (target == null) return;
        _instance._target = target;
        bool isFarFromTarget = true;
        while (_instance._target != null &&
         !_instance._followingCancellationTokenSource.IsCancellationRequested
         && isFarFromTarget)
        {
            _instance._offset = CurrentZoomProfile;
            Vector3 desiredPosition = new Vector3(_instance._target.position.x + _instance._offset.x, _instance._target.position.y + _instance._offset.y, _instance._target.position.z + _instance._offset.z);
            Vector3 smoothPosition = Vector3.Lerp(_instance.transform.position, desiredPosition, _instance._smoothSpeed * Time.deltaTime);
            _instance.transform.position = smoothPosition;
            isFarFromTarget = Vector3.Distance(_instance.transform.position, desiredPosition) > 0.1f;
            await Task.Yield();
        }
        await Task.CompletedTask;
    }
    public static Task FollowTargetAsync(Transform target = null)
    {
        _followTask = FollowTarget(target);
        _followTask.ConfigureAwait(false);
        return _followTask;
    }

    private static async Task ZoomIn()
    {
        if (_instance._currentZoomProfile == 0) return;
        _instance._currentZoomProfile--;
        _instance._offset = _instance._zoomProfiles.zooms[_instance._currentZoomProfile];
        bool isFarFromTarget = true;
        while (!_instance._zoomCancellationTokenSource.IsCancellationRequested &&
        isFarFromTarget)
        {
            //lerp zoom
            _instance.transform.position = Vector3.Lerp(_instance.transform.position, _instance._target.position + _instance._offset, _instance._zoomSpeed * Time.deltaTime);
            isFarFromTarget = Vector3.Distance(_instance.transform.position, _instance._target.position + _instance._offset) > 0.1f;
            await Task.Yield();
        }
        await Task.CompletedTask;
    }
    public static Task ZoomInAsync()
    {
        _zoomTask = ZoomIn();
        _zoomTask.ConfigureAwait(false);
        return _zoomTask;
    }

    public static async Task ZoomOut()
    {
        if (_instance._currentZoomProfile == _instance._zoomProfiles.zooms.Length - 1) return;
        _instance._currentZoomProfile++;
        _instance._offset = _instance._zoomProfiles.zooms[_instance._currentZoomProfile];
        bool isFarFromTarget = true;
        while (!_instance._zoomCancellationTokenSource.IsCancellationRequested &&
        isFarFromTarget)
        {
            //lerp zoom
            _instance.transform.position = Vector3.Lerp(_instance.transform.position, _instance._target.position + _instance._offset, _instance._zoomSpeed * Time.deltaTime);
            isFarFromTarget = Vector3.Distance(_instance.transform.position, _instance._target.position + _instance._offset) > 0.1f;
            await Task.Yield();
        }
        await Task.CompletedTask;
    }

    public static Task ZoomOutAsync()
    {
        if (_instance._currentZoomProfile == _instance._zoomProfiles.zooms.Length - 1) return Task.CompletedTask;
        _zoomTask = ZoomOut();
        _zoomTask.ConfigureAwait(false);
        return _zoomTask;
    }

    void OnEnable()
    {
        _followingCancellationTokenSource = new CancellationTokenSource();
        _zoomCancellationTokenSource = new CancellationTokenSource();
    }
    void OnDisable()
    {
        _followingCancellationTokenSource.Cancel();
        _zoomCancellationTokenSource.Cancel();
        _followTask = null;
        _zoomTask = null;
    }

    void OnDestroy()
    {
        _followingCancellationTokenSource.Dispose();
        _zoomCancellationTokenSource.Dispose();
    }
}

[Serializable]
public struct ZoomProfiles
{
    public Vector3[] zooms;
}
