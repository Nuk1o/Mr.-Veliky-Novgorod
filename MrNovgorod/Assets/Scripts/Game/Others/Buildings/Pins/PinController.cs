using Game.Hud;
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
        [SerializeField] private Button _button;
        
        private UINavigator _uiNavigator;
        private HUDCloseButtonPresenter _button1;
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

        public void Setup(BuildingData buildingData, UINavigator uiNavigator, HUDCloseButtonPresenter hudCloseButtonPresenter, LandmarkModel landmarkModel = null)
        {
            _buildingData = buildingData;
            _landmarkModel = landmarkModel;
            _uiNavigator = uiNavigator;
            _button1 = hudCloseButtonPresenter;
        }

        private void OpenInformationWindow()
        {
            var presenter = _uiNavigator
                .Show<AttractionInformationWindowPresenter, AttractionInformationWindowView>()
                .AsScreen();
            
            presenter.Presenter.SetupBuildingModel(_buildingData);
        }
    }
}