using System;
using Game.Buildings;
using Game.Landmarks.Model;
using GameCore.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Hud.AttractionInformationWindow
{
    public class AttractionInformationWindowPresenter : UISystemPresenter<AttractionInformationWindowView>, IDisposable
    {
        [Inject] private ImageLoader _imageLoader;
        private readonly AttractionInformationWindowView _view;
        private CompositeDisposable _disposables;
        
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
        }

        public void SetupImages(Ebuildings buildingID)
        {
            var sprites = _imageLoader.GetSprites();
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