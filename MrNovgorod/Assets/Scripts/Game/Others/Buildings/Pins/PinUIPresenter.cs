using System;
using GameCore.UI;
using UniRx;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Game.Buildings.Pins
{
    public class PinUIPresenter : IInitializable, IDisposable
    {
        [Inject] private UINavigator _uiNavigator;
        private CompositeDisposable _disposable;
        private readonly PinUIView _view;
        
        public PinUIPresenter(PinUIView view)
        {
            _view = view;
        }
        
        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            _view.PinClickButton
                .Subscribe(_ => OpenView())
                .AddTo(_disposable);
        }

        private void OpenView()
        {
            var screen = _uiNavigator.Show<BuildingUIPresenter,BuildingUIView>().AsScreen();
            screen.Presenter.BeforeShowView(_view.BuildingData);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}