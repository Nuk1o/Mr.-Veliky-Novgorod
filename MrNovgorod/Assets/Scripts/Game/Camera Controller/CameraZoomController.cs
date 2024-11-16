using System;
using Cinemachine;
using UniRx;
using UnityEngine;

namespace GameCore
{
    public class CameraZoomController : MonoBehaviour, IDisposable
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private float _zoomSpeed = 0.5f;
        [SerializeField] private float _minZoom = 2f;
        [SerializeField] private float _maxZoom = 10f;
        [SerializeField] private float _smoothTime = 0.1f;

        private CompositeDisposable _disposable;
        private float _currentZoom;
        private float _targetZoom;
        
        public ReactiveProperty<bool> IsZooming;

        private void Start()
        {
            _currentZoom = _virtualCamera.m_Lens.FieldOfView;
            _targetZoom = _currentZoom;
            _disposable = new CompositeDisposable();

            Observable
                .EveryFixedUpdate()
                .Subscribe(_ => ZoomCamera())
                .AddTo(_disposable);
        }

        private void ZoomCamera()
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                float currentTouchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                float previousTouchDistance = Vector2.Distance(touchZero.position - touchZero.deltaPosition,
                    touchOne.position - touchOne.deltaPosition);

                float deltaDistance = currentTouchDistance - previousTouchDistance;

                _targetZoom -= deltaDistance * _zoomSpeed;
                _targetZoom = Mathf.Clamp(_targetZoom, _minZoom, _maxZoom);
                IsZooming.Value = true;
            }
            else
            {
                IsZooming.Value = false;
            }
            
            if (Input.mouseScrollDelta.y != 0)
            {
                _targetZoom -= Input.mouseScrollDelta.y * _zoomSpeed;
                _targetZoom = Mathf.Clamp(_targetZoom, _minZoom, _maxZoom);
                IsZooming.Value = true;
            }

            _currentZoom = Mathf.Lerp(_currentZoom, _targetZoom, _smoothTime);
            _virtualCamera.m_Lens.FieldOfView = _currentZoom;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
