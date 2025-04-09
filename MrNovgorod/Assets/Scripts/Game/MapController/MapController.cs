using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MapController
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private List<RectTransform> _mapRects;
        [Header("Buttons")]
        [SerializeField] private Button _zoomInButton;
        [SerializeField] private Button _zoomOutButton;
        
        private CompositeDisposable _disposables;
        private ReactiveProperty<RectTransform> _currentMapRect;

        private void Start()
        {
            _disposables = new CompositeDisposable();
            _currentMapRect = new ReactiveProperty<RectTransform>();

            SetupStartMap();
            UpdateMap();
            
            _zoomOutButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (_scrollRect.content != _mapRects.First())
                {
                    var currentIndex = _mapRects.FindIndex(map => map == _currentMapRect.Value);
                    _currentMapRect.Value = _mapRects[currentIndex - 1];
                }
            }).AddTo(_disposables);
            
            _zoomInButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (_scrollRect.content != _mapRects.Last())
                {
                    var currentIndex = _mapRects.FindIndex(map => map == _currentMapRect.Value);
                    _currentMapRect.Value = _mapRects[currentIndex + 1];
                }
            }).AddTo(_disposables);
        }

        private void SetupStartMap()
        {
            _currentMapRect.Value = _mapRects.First();
            _scrollRect.content = _currentMapRect.Value;
        }

        private void UpdateMap()
        {
            _currentMapRect.Subscribe(map =>
            {
                foreach (var variableRect in _mapRects)
                {
                    variableRect.gameObject.SetActive(false);
                }
                map.gameObject.SetActive(true);
                _scrollRect.content = map;
            }).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}