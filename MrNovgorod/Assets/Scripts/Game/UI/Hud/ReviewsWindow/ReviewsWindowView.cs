using System;
using Cysharp.Threading.Tasks;
using Game.Landmarks.Model;
using Game.Others.Tools;
using GameCore.UI;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Hud.ReviewsWindow
{
    public class ReviewsWindowView : UISystemView
    {
        [SerializeField] private Button _sendButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private StarRating _starRating;

        [SerializeField] private ReviewPrefab _reviewPrefab;
        [SerializeField] private Transform _reviewsContainer;

        public IObservable<Unit> SendClickButton => _sendButton.OnClickAsObservable();
        public IObservable<Unit> BackClickButton => _backButton.OnClickAsObservable();
        
        public override void Initialize()
        {
            
        }

        public async UniTask SetReviews(LandmarkModel landmarkModel, ImageLoader imageLoader)
        {
            foreach (var review in landmarkModel.Reviews)
            {
                var reviewPrefab = Instantiate(_reviewPrefab, _reviewsContainer);
                var sprite = await imageLoader.LoadSpriteAsync(review.UserAvatar);
                reviewPrefab.SetData(review,sprite);
            }
        }
        
        public ReviewData GetReviewData()
        {
            var reviewData = new ReviewData
            {
                comment = _inputField.text,
                rating = (int)_starRating.GetRating()
            };
            return reviewData;
        }
    }

    public struct ReviewData
    {
        public string comment;
        public int rating;
    }
}