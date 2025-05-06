using System;
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
            SetupImages();
        }

        private void SetupImages()
        {
            var sprites = _imageLoader.GetSprites();
            Debug.Log($"Aboba load sprites: {sprites.Count}");
            _view.GenerateImages(sprites);
        }

        public void SetupBuildingModel(BuildingData buildingData = null, LandmarkModel landmarkModel = null)
        {
            if (buildingData != null)
            {
                _view.SetName(buildingData.NameBuilding);
                _view.SetDescription(buildingData.DescriptionBuilding);
            }

            if (landmarkModel != null)
            {
                _view.SetName(landmarkModel.NameBuilding);
                _view.SetDescription(landmarkModel.DescriptionBuilding);
            }
        }

        private void OnExitClick()
        {
            _view.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            
        }
    }
}