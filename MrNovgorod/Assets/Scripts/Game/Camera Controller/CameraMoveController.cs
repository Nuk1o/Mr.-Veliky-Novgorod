using System;
using Cinemachine;
using UniRx;
using UnityEngine;

namespace GameCore.Camera
{
    public class CameraMoveController : MonoBehaviour, IDisposable
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private float _cameraSpeed = 10f;
        [SerializeField] private float _smoothTime = 0.1f;

        [Space] [Header("Map restriction")] [SerializeField]
        private float _minX = -10f;

        [SerializeField] private float _maxX = 10f;
        [SerializeField] private float _minZ = -10f;
        [SerializeField] private float _maxZ = 10f;

        private CompositeDisposable _disposables;
        private Vector3 _targetPosition;
        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            _disposables = new CompositeDisposable();
            _targetPosition = _camera.transform.position;

            Observable
                .EveryFixedUpdate()
                .Subscribe(_ => CameraMovementControls())
                .AddTo(_disposables);
        }

        private void CameraMovementControls()
        {
            if (FindObjectOfType<CameraZoomController>().IsZooming.Value)
                return;


            if (Input.touchCount <= 0 && !Input.GetMouseButton(0)) return;
            Vector2 touchDelta;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    touchDelta = touch.deltaPosition;
                }
                else
                {
                    return;
                }
            }
            else
            {
                touchDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 100;
            }

            float deltaX = -touchDelta.x * _cameraSpeed * Time.fixedDeltaTime;
            float deltaZ = -touchDelta.y * _cameraSpeed * Time.fixedDeltaTime;

            _targetPosition.x += deltaX;
            _targetPosition.z += deltaZ;

            _targetPosition.x = Mathf.Clamp(_targetPosition.x, _minX, _maxX);
            _targetPosition.z = Mathf.Clamp(_targetPosition.z, _minZ, _maxZ);

            _camera.transform.position =
                Vector3.SmoothDamp(_camera.transform.position, _targetPosition, ref _velocity, _smoothTime);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}