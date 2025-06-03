using System;
using Game.Buildings;
using Game.Hud.ReviewsWindow;
using Game.Landmarks.Model;
using Game.Others.Tools;
using Game.UI.Popup;
using GameCore.UI;
using UniRx;
using UnityEngine;
using UserServerService.Config;
using Zenject;

namespace Game.Hud.AttractionInformationWindow
{
    public class AttractionInformationWindowPresenter : UISystemPresenter<AttractionInformationWindowView>, IDisposable
    {
        [Inject] private LandmarksImageLoader _landmarksImageLoader;
        [Inject] private PopupPresenter _popupPresenter;
        [Inject] private UINavigator _uiNavigator;
        private readonly AttractionInformationWindowView _view;
        private CompositeDisposable _disposables;
        
        private LandmarkModel _currentLandmark;
        
        public AttractionInformationWindowPresenter(AttractionInformationWindowView view) : base(view)
        {
            _view = view;
        }

        public override void Initialize()
        {
            Debug.Log($"Initializing AttractionInformationWindowPresenter");
            _disposables = new CompositeDisposable();
        }

        protected override void BeforeShow()
        {
            _disposables = new CompositeDisposable();
            
            _view.CloseClickButton
                .Subscribe(_ => OnExitClick())
                .AddTo(_disposables);
            
            _view.MapClickButton
                .Subscribe(_ => Application.OpenURL($"{ServerConfig.SITE_ADRESS}?building={_currentLandmark.serverId}"))
                .AddTo(_disposables);
            
            _view.CoordClickButton
                .Subscribe(_ => OnCoordClick())
                .AddTo(_disposables);
            
            _view.ReviewClickButton
                .Subscribe(_ =>
                {
                    var screen = _uiNavigator.Show<ReviewsWindowPresenter, ReviewsWindowView>().AsScreen();
                    screen.Presenter.SetLandmarks(_currentLandmark);
                })
                .AddTo(_disposables);
        }

        private void OnCoordClick()
        {
            GUIUtility.systemCopyBuffer = _currentLandmark?.GlobalCoordinatesBuilding;
            _popupPresenter.ShowPopup("Координаты скопированы!");
        }

        public void SetupImages(Ebuildings buildingID)
        {
            var sprites = _landmarksImageLoader.GetSprites();
            _view.GenerateImages(buildingID,sprites);
        }

        public void SetupBuildingModel(BuildingData buildingData)
        {
            if (buildingData == null)
                return;
            
            _view.SetName(buildingData.NameBuilding);
            _view.SetDescription(buildingData.DescriptionBuilding);
        }
        
        public void SetupBuildingModel(LandmarkModel landmarkModel)
        {
            if (landmarkModel == null)
                return;
            
            _view.SetName(landmarkModel.NameBuilding);
            _view.SetDescription(landmarkModel.DescriptionBuilding);
            _view.SetHistory(landmarkModel.HistoryBuilding);
            _currentLandmark = landmarkModel;
        }

        private void OnExitClick()
        {
            _view.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}