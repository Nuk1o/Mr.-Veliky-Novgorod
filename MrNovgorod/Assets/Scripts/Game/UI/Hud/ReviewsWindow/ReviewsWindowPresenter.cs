using System;
using Game.Landmarks.Model;
using Game.Others.Tools;
using Game.UI.Popup;
using Game.User;
using GameCore.UI;
using Server.UserServerService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Hud.ReviewsWindow
{
    public class ReviewsWindowPresenter : UISystemPresenter<ReviewsWindowView>, IDisposable
    {
        [Inject] private ImageLoader _imageLoader;
        [Inject] private IUserServerService _serverController;
        [Inject] private UserModel _userModel;
        [Inject] private PopupPresenter _popupPresenter;
        private readonly ReviewsWindowView _view;
        private CompositeDisposable _disposables;
        private LandmarkModel _landmarkModel;
        
        public ReviewsWindowPresenter(ReviewsWindowView view) : base(view)
        {
            _view = view;
        }

        public override void Initialize()
        {
            Debug.Log($"Initializing ReviewsWindowPresenter");
            _disposables = new CompositeDisposable();
        }

        protected override void BeforeShow()
        {
            _disposables = new CompositeDisposable();
            
            _view.BackClickButton
                .Subscribe(_ => OnExitClick())
                .AddTo(_disposables);
            
            _view.SendClickButton
                .Subscribe(_ => SendReview())
                .AddTo(_disposables);
        }

        public async void SetLandmarks(LandmarkModel landmarkModel)
        {
            _landmarkModel = landmarkModel;
            await _view.SetReviews(landmarkModel,_imageLoader);
        }
        
        private void SendReview()
        {
            if (!_userModel.IsLoggedIn)
            {
                _popupPresenter.ShowPopup("Для отправки отзыва требуется авторизация!");
                return;
            }
            var data = _view.GetReviewData();
            if (data.rating == 0)
            {
                _popupPresenter.ShowPopup("Некорректный рейтинг.");
                return;
            }
            _serverController.SendReview(_landmarkModel,data);
            _view.SendLocalReviews(_imageLoader, _userModel);
        }
        
        private void OnExitClick()
        {
            _view.gameObject.SetActive(false);
            _disposables?.Dispose();
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}