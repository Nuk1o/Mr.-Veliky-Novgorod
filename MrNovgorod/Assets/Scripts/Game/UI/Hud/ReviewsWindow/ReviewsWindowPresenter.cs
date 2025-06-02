using System;
using GameCore.UI;
using UniRx;
using UnityEngine;

namespace Game.Hud.ReviewsWindow
{
    public class ReviewsWindowPresenter : UISystemPresenter<ReviewsWindowView>, IDisposable
    {
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