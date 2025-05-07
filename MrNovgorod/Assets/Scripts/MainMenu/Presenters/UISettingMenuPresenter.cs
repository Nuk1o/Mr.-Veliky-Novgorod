using System;
using MainMenu.Views;
using Server.ServerDataProviders.UserServerDataProvider;
using UniRx;
using UnityEngine;
using Zenject;

namespace MainMenu.Presenters
{
    public class UISettingMenuPresenter : IInitializable, IDisposable
    {
        [Inject] private UserServerDataProvider _serverController;
        private CompositeDisposable _disposable;
        private readonly UISettingMenuView _view;
        
        public UISettingMenuPresenter(UISettingMenuView view)
        {
            _view = view;
        }
        
        public void Initialize()
        {
            _disposable = new CompositeDisposable();
#if DEBUG_LOG
            Debug.Log($"SettingMenu initialized");
#endif
            //_serverController.Test(_disposable);
            //_serverController.TestLogin(_disposable);
            
            _view.CloseButtonClick
                .Subscribe(_ => _view.gameObject.SetActive(false))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void ShowSettingsMenu()
        {
            _view.gameObject.SetActive(true);
            _view.LoadScreen();
        }
    }
}