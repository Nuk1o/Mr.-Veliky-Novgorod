using UnityEngine;
using UniRx;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _zoomspeed = 2f;
    [SerializeField] private float _minZoom = 2f;
    [SerializeField] private float _maxZoom = 20f;

    private bool _isDragging = false;
    private Vector3 _lastMousePosition;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => StartDragging())
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => StopDragging())
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => Input.touchCount > 0)
            .Subscribe(_ => HandleTouchInput())
            .AddTo(this);
    }

    private void Update()
    {
        if (_isDragging)
        {
            Vector3 delta = Input.mousePosition - _lastMousePosition;
            MoveCamera(delta);
        }
        _lastMousePosition = Input.mousePosition;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
            ZoomCamera(scroll);
    }

    private void StartDragging()
    {
        _isDragging = true;
        _lastMousePosition = Input.mousePosition;
    }

    private void StopDragging()
    {
        _isDragging = false;
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved && _isDragging)
            {
                MoveCamera(touch.deltaPosition);
            }
            else if (touch.phase == TouchPhase.Began)
            {
                StartDragging();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StopDragging();
            }
        }
    }

    private void MoveCamera(Vector3 delta)
    {
        var moveX = -delta.x * _speed * Time.deltaTime;
        var moveZ = -delta.y * _speed * Time.deltaTime;

        transform.position += new Vector3(moveX, 0, moveZ);
    }

    private void ZoomCamera(float scroll)
    {
        _camera.fieldOfView -= scroll * _zoomspeed;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, _minZoom, _maxZoom);
    }
}