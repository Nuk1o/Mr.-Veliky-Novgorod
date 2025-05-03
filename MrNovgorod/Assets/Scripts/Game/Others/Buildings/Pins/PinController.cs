using Game.Hud.AttractionInformationWindow;
using Game.Landmarks.Model;
using GameCore.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Buildings.Pins
{
    public class PinController : MonoBehaviour
    {
        [Inject] private UINavigator _uiNavigator;
        [Inject] private AttractionInformationWindowPresenter _attractionInformation;
        
        [SerializeField] private Button _button;
        
        private BuildingData _buildingData;
        private LandmarkModel _landmarkModel;
        private CompositeDisposable _compositeDisposable;

        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();
            _button.OnClickAsObservable()
                .Subscribe(_ => OpenInformationWindow())
                .AddTo(_compositeDisposable);
        }

        public void Setup(BuildingData buildingData = null, LandmarkModel landmarkModel = null)
        {
            _buildingData = buildingData;
            _landmarkModel = landmarkModel;
        }

        private void OpenInformationWindow()
        {
            _attractionInformation.Initialize();
            //_uiNavigator.Show<AttractionInformationWindowPresenter, AttractionInformationWindowView>().AsScreen().WithHUD();
        }
    }
}