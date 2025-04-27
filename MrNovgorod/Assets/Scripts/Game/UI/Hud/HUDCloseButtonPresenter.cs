using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Hud
{
    public class HUDCloseButtonPresenter : IInitializable, IDisposable
    {
        [Inject] private SceneLoader _sceneLoader;
        private HUDCloseButtonView _view;
        private CompositeDisposable _disposable;
        
        public HUDCloseButtonPresenter(HUDCloseButtonView view)
        {
            _view = view;
        }
        
        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            _view.CloseClickButton
                .Subscribe(_ =>OnExitMenuClick())
                .AddTo(_disposable);
        }
        
        private void OnExitMenuClick()
        {
            _sceneLoader.LoadSceneAsync("MainMenu")
                .Subscribe(_ => { Debug.Log("Сцена успешно загружена"); });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}