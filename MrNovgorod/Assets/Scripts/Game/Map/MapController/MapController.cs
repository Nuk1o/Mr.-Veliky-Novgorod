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
        [Header("Pins")]
        [SerializeField] private List<GameObject> _pinsContainers;
        [SerializeField] private BuildingsData _buildings;
        [SerializeField] private GameObject _pinPrefab;
        [Header("Buttons")]
        [SerializeField] private Button _zoomInButton;
        [SerializeField] private Button _zoomOutButton;

        private CompositeDisposable _disposables;
        private ReactiveProperty<RectTransform> _currentMapRect;
        private List<TransitionData> _transitions;

        private void Start()
        {
            _disposables = new CompositeDisposable();
            _currentMapRect = new ReactiveProperty<RectTransform>();

            InitializeTransitions();
            SetupStartMap();
            SetupPinsOnMaps();
            UpdateMap();

            _zoomOutButton.OnClickAsObservable().Subscribe(_ =>
            {
                ZoomOut();
            }).AddTo(_disposables);

            _zoomInButton.OnClickAsObservable().Subscribe(_ =>
            {
                ZoomIn();
            }).AddTo(_disposables);
        }

        private void SetupPinsOnMaps()
        {
            for (var index = 0; index < _pinsContainers.Count; index++)
            {
                foreach (var building in _buildings.Buildings)
                {
                    var pin = Instantiate(_pinPrefab,building.Value.BuildingPositions[index], Quaternion.identity,_pinsContainers[index].transform);
                    var rectTransform = pin.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = building.Value.BuildingPositions[index];
                }
            }
        }

        private void ZoomOut()
        {
            if (_scrollRect.content == _mapRects.First())
                return;
            
            var currentIndex = _mapRects.FindIndex(map => map == _currentMapRect.Value);
            var transition = _transitions[currentIndex - 1];
            var currentPosition = _currentMapRect.Value.position;

            var kxReverse = 1 / transition.Kx;
            var bxReverse = -transition.Bx / transition.Kx;
            var kyReverse = 1 / transition.Ky;
            var byReverse = -transition.By / transition.Ky;

            var newPosition = new Vector3(
                kxReverse * currentPosition.x + bxReverse,
                kyReverse * currentPosition.y + byReverse,
                0
            );

            _currentMapRect.Value = _mapRects[currentIndex - 1];
            _currentMapRect.Value.position = newPosition;
        }

        private void ZoomIn()
        {
            if (_scrollRect.content == _mapRects.Last())
                return;
            
            var currentIndex = _mapRects.FindIndex(map => map == _currentMapRect.Value);
            var transition = _transitions[currentIndex];
            var currentPosition = _currentMapRect.Value.position;

            var newPosition = new Vector3(
                transition.Kx * currentPosition.x + transition.Bx,
                transition.Ky * currentPosition.y + transition.By,
                0
            );

            _currentMapRect.Value = _mapRects[currentIndex + 1];
            _currentMapRect.Value.position = newPosition;
        }

        private void InitializeTransitions()
        {
            _transitions = new List<TransitionData>
            {
                new() { Kx = 1.6015625f, Ky = 1.64f, Bx = 56.96875f, By = -247.32f }, // Map1→Map2
                new() { Kx = 3.2585f, Ky = 2.6667f, Bx = -76.0f, By = 471.67f },    // Map2→Map3
                new() { Kx = 2.281f, Ky = 2.130f, Bx = -37.334f, By = -461.81f }    // Map3→Map4
            };
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
                foreach (var rect in _mapRects)
                {
                    rect.gameObject.SetActive(false);
                }
                map.gameObject.SetActive(true);
                _scrollRect.content = map;
            }).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables?.Dispose();
        }

        private class TransitionData
        {
            public float Kx;
            public float Ky;
            public float Bx;
            public float By;
        }
    }
}