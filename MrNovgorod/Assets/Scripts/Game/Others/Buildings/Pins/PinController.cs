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
        private BuildingData _buildingData;
        private LandmarkModel _landmarkModel;
        private CompositeDisposable _compositeDisposable;

        private Ebuildings _buildingID;

        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();
            _button.OnClickAsObservable()
                .Subscribe(_ => OpenInformationWindow())
                .AddTo(_compositeDisposable);
        }

        public void Setup(Ebuildings buildingID, BuildingData buildingData, UINavigator uiNavigator)
        {
            _buildingData = buildingData;
            _uiNavigator = uiNavigator;
            _buildingID = buildingID;
        }
        
        public void Setup(Ebuildings buildingID, LandmarkModel landmarkModel, UINavigator uiNavigator)
        {
            _landmarkModel = landmarkModel;
            _uiNavigator = uiNavigator;
            _buildingID = buildingID;
        }

        private void OpenInformationWindow()
        {
            var presenter = _uiNavigator
                .Show<AttractionInformationWindowPresenter, AttractionInformationWindowView>()
                .AsScreen().Presenter;

            if (_landmarkModel != null)
            {
                presenter.SetupBuildingModel(_landmarkModel);
            }
            else
            {
                presenter.SetupBuildingModel(_buildingData);
            }

            presenter.SetupImages(_buildingID);
        }
    }
}