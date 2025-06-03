using System;
using Game.Landmarks.Model;
using Game.Others.Tools;
using GameCore.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Hud.ReviewsWindow
{
    public class ReviewsWindowPresenter : UISystemPresenter<ReviewsWindowView>, IDisposable
    {
        [Inject] private LandmarksModel _landmarksModel;
        [Inject] private ImageLoader _imageLoader;
        private readonly ReviewsWindowView _view;
        private CompositeDisposable _disposables;
        
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
            await _view.SetReviews(landmarkModel,_imageLoader);
        }
        
        private void SendReview()
        {
            
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