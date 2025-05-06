using System.Collections.Generic;
using System.Linq;
using Game.Buildings;
using Game.Buildings.Pins;
using Game.Hud;
using Game.Landmarks.Interface;
using Game.Landmarks.Model;
using GameCore.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.MapController
{
    public class MapController : MonoBehaviour
    {
        [Inject] private LandmarksModel _landmarksServerModel;
        [Inject] private UINavigator _uiNavigator;
        [Inject] private HUDCloseButtonPresenter _button1;
        
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private List<RectTransform> _mapRects;
        [Header("Pins")]
        [SerializeField] private List<GameObject> _pinsContainers;
        [SerializeField] private BuildingsData _landmarksLocalModel;
        [SerializeField] private GameObject _pinPrefab;
        [Header("Buttons")]
        [SerializeField] private Button _zoomInButton;
        [SerializeField] private Button _zoomOutButton;

        private CompositeDisposable _disposables;
        private ReactiveProperty<RectTransform> _currentMapRect;
        private List<TransitionData> _transitions;
        private DiContainer _container;

        private void Start()
        {
            _disposables = new CompositeDisposable();
            _currentMapRect = new ReactiveProperty<RectTransform>();
            _container = new DiContainer();

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
#if SERVER_ON
                foreach (var building in _landmarksServerModel.Buildings)
                {
                    var pin = CreatePinOnMap(building,index);
                    var controller = pin.GetComponent<PinController>();
                    controller.Setup(building.Key, building.Value, _uiNavigator);
                }
#else
                foreach (var building in _landmarksLocalModel.Buildings)
                {
                    var pin = CreatePinOnMap(building,index);
                    var controller = pin.GetComponent<PinController>();
                    controller.Setup(building.Key, building.Value, _uiNavigator);
                }
#endif
            }
        }

        private GameObject CreatePinOnMap<T>(KeyValuePair<Ebuildings, T> building, int index) 
            where T : IBuildingPositionProvider
        {
            var position3D = building.Value.BuildingPositions[index];
            var position2D = new Vector2(position3D.x, position3D.y);

            var pin = _container.InstantiatePrefab(_pinPrefab, 
                position2D, 
                Quaternion.identity,
                _pinsContainers[index].transform);
            
            var rectTransform = pin.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = position2D;
            return pin;
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