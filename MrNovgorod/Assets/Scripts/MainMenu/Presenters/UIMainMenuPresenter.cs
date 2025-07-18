﻿using MainMenu.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace MainMenu.Presenters
{
    public class UIMainMenuPresenter
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] UISettingMenuPresenter _uiSettingMenuPresenter;
        private readonly UIMainMenuView _view;
        private CompositeDisposable _disposables;

        public UIMainMenuPresenter(UIMainMenuView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _disposables = new CompositeDisposable();
#if DEBUG_LOG
            Debug.Log("UIMainMenuPresenter initialized");
#endif

            SubscribeToButtonClicks();
            SceneLoaderInitialized();
        }

        private void SubscribeToButtonClicks()
        {
#if DEBUG_LOG
            Debug.Log("Subscribing to button clicks");
#endif

            _view.StartClickButton.Subscribe(_ => OnStartGameClicked()).AddTo(_view);
            _view.SettingClickButton.Subscribe(_ => OnSettingsClicked()).AddTo(_view);
            _view.ExitClickButton.Subscribe(_ => OnExitGameClicked()).AddTo(_view);
        }

        private void SceneLoaderInitialized()
        {
            _sceneLoader.LoadProgress
                .Subscribe(progress => { Debug.Log($"Загрузка сцены: {progress * 100}%"); })
                .AddTo(_disposables);

            _sceneLoader.IsLoading
                .Where(isLoading => isLoading)
                .Subscribe(_ => { Debug.Log("Загрузка сцены началась"); })
                .AddTo(_disposables);
        }

        private void OnStartGameClicked()
        {
            _sceneLoader.LoadSceneAsync("NewMap")
                .Subscribe(_ => { Debug.Log("Сцена успешно загружена"); });
        }

        private void OnSettingsClicked()
        {
            _uiSettingMenuPresenter.ShowSettingsMenu();
        }

        private void OnExitGameClicked()
        {
            Application.Quit();
        }
    }
}