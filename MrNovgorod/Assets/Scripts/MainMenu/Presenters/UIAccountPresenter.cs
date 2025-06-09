using System;
using Game.Others.Tools;
using Game.User;
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
        [Inject] private ImageLoader _imageLoader;
        [Inject] private UserModel _userModel;
        private CompositeDisposable _disposable;
        private readonly UIAccountView _view;

        private EAccountWindows _currentWindow;

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
                if (_currentWindow == EAccountWindows.Authorization)
                {
                    UserLogin();
                }
                else
                {
                    _currentWindow = EAccountWindows.Authorization;
                    _view.OpenWindow(EAccountWindows.Authorization);
                }
            }).AddTo(_disposable);

            _view.RegisterButtonClick.Subscribe(_ =>
            {
                if (_currentWindow == EAccountWindows.Registration)
                {
                    RegisterUser();
                }
                else
                {
                    _currentWindow = EAccountWindows.Registration;
                    _view.OpenWindow(EAccountWindows.Registration);
                }
            }).AddTo(_disposable);

            var loadedUser = LoadUser();
            if (loadedUser != null)
            {
                _currentWindow = EAccountWindows.Profile;
                _view.OpenWindow(EAccountWindows.Profile);
                LoginProfile(loadedUser.token);
            }
            else
            {
                _currentWindow = EAccountWindows.Registration;
                _view.OpenWindow(EAccountWindows.Registration);
            }
        }

        private void OpenProfile(ServerUserModel profileServerData)
        {
            Debug.Log($"ABOBA OPEN PROFILE: {profileServerData}");
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

        private async void UserLogin()
        {
            UserLoginData loginData = new()
            {
                email = _view.EmailInputField.text,
                password = _view.PasswordInputField.text
            };

            var authorizationServerData = await _serverController.LoginUser(loginData);
            var user = new UserPrefsAccount()
            {
                login = _view.NameInputField.text,
                email = _view.EmailInputField.text,
                token = authorizationServerData.token,
            };
            SaveUser(user);
            LoginProfile(authorizationServerData.token);
        }

        private async void LoginProfile(string token)
        {
            var profileServerData = await _serverController.GetUserData(token);

            _currentWindow = EAccountWindows.Profile;
            _view.OpenWindow(EAccountWindows.Profile);
            OpenProfile(profileServerData);
            if (profileServerData.avatar != "")
            {
                _userModel.avatar = profileServerData.avatar;
                _userModel.name = profileServerData.name;
                var avatar = await _imageLoader.LoadSpriteAsync(profileServerData.avatar);
                _view.SetDataProfile(profileServerData, avatar);
            }
            else
            {
                _view.SetDataProfile(profileServerData, null);
            }
        }

        private void SaveUser(UserPrefsAccount user)
        {
            string jsonData = JsonUtility.ToJson(user);

            PlayerPrefs.SetString("user_data", jsonData);
            PlayerPrefs.Save();
        }

        public static UserPrefsAccount LoadUser()
        {
            if (PlayerPrefs.HasKey("user_data"))
            {
                string jsonData = PlayerPrefs.GetString("user_data");

                UserPrefsAccount user = JsonUtility.FromJson<UserPrefsAccount>(jsonData);
                return user;
            }
            else
            {
                Debug.Log("Empty save data");
                return null;
            }
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

[Serializable]
public class UserPrefsAccount
{
    public string token;
    public string login;
    public string email;
}