using System;
using MainMenu.Views;
using Server.UserServerService;
using Server.UserServerService.Data;
using UniRx;
using UnityEngine;
using Zenject;

namespace MainMenu.Presenters
{
    public class UIAccountPresenter : IInitializable, IDisposable
    {
        [Inject] private IUserServerService _serverController;
        private CompositeDisposable _disposable;
        private readonly UIAccountView _view;
        
        public UIAccountPresenter(UIAccountView view)
        {
            _view = view;
        }
        
        public void Initialize()
        {
            _disposable = new CompositeDisposable();
#if DEBUG_LOG
            Debug.Log($"SettingMenu initialized");
#endif
            _view.CloseButtonClick
                .Subscribe(_ => _view.gameObject.SetActive(false))
                .AddTo(_disposable);

            _view.LoginButtonClick.Subscribe(_ =>
            {
                UserLogin();
            }).AddTo(_disposable);
            
            _view.RegisterButtonClick.Subscribe(_ =>
            {
                RegisterUser();
            }).AddTo(_disposable);
        }

        private void RegisterUser()
        {
            UserRegisterData registerData = new()
            {
                name = _view.NameInputField.text,
                email = _view.EmailInputField.text,
                password = _view.PasswordInputField.text,
                confirm_password = _view.PasswordInputField.text
            };
            _serverController.RegisterUser(registerData);
        }

        private void UserLogin()
        {
            UserLoginData loginData = new()
            {
                email = _view.EmailInputField.text,
                password = _view.PasswordInputField.text
            };
            _serverController.LoginUser(loginData);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void ShowSettingsMenu()
        {
            _view.gameObject.SetActive(true);
        }
    }
}