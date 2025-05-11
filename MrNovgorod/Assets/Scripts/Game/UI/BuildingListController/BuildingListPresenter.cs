using System;
using System.Collections.Generic;
using Game.Buildings;
using Game.Hud.AttractionInformationWindow;
using Game.Landmarks.Model;
using GameCore.UI;
using UniRx;
using Zenject;

namespace Game.UI.BuildingListController
{
    public class BuildingListPresenter : IInitializable, IDisposable
    {
        [Inject] private LandmarksModel _landmarksServerModel;
        [Inject] private UINavigator _uiNavigator;
        
        private BuildingListView _view;
        private CompositeDisposable _disposable;
        private List<SightView> _sightViews;

        public BuildingListPresenter(BuildingListView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            _sightViews = new List<SightView>();
            
            _view.MenuClickButton.Subscribe(_ =>
            {
                _view.SetActiveMenu(!_view.MenuIsActive);
            }).AddTo(_disposable);
            
            FillingBuildingList();
        }

        private void FillingBuildingList()
        {
            if (_landmarksServerModel == null)
                return;
            
            if (_sightViews.Count != 0)
                return;
            
            foreach (var building in _landmarksServerModel.Buildings)
            {
                var sightView = _view.SpawnSightObject();
                sightView.SetupLandmark(building.Value.NameBuilding, building.Value.ImageBuilding);
                sightView.OnOpenLandmarkAsObservable().Subscribe(_ =>
                {
                    OpenLandmark(building);
                }).AddTo(_disposable);
            }
        }

        private void OpenLandmark(KeyValuePair<Ebuildings, LandmarkModel> building)
        {
            var presenter = _uiNavigator
                .Show<AttractionInformationWindowPresenter, AttractionInformationWindowView>()
                .AsScreen().Presenter;
            presenter.SetupBuildingModel(building.Value);
            presenter.SetupImages(building.Key);
            _view.SetActiveMenu(!_view.MenuIsActive);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}