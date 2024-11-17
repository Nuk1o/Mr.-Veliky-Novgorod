using System;
using GameCore.UI;
using UniRx;

namespace Game.Buildings
{
    public class BuildingUIPresenter : UISystemPresenter<BuildingUIView>, IDisposable
    {
        private BuildingUIView _view;
        private CompositeDisposable _disposables;
        
        public BuildingUIPresenter(BuildingUIView view) : base(view)
        {
            _view = view;
        }
        
        public void Initialize()
        {
            _disposables = new CompositeDisposable();
            _view.CloseClickButton
                .Subscribe(_ => _view.SetActive(false))
                .AddTo(_disposables);
        }

        public void BeforeShowView(BuildingData buildingData)
        {
            _view.BeforeShow();
            _view.SetDataBuilding(buildingData.ImageBuilding, buildingData.LogoText, buildingData.DescriptionText, buildingData.HistoryText);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}