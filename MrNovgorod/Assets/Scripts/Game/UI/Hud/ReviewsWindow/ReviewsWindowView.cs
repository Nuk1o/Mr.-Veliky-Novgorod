using System;
using Cysharp.Threading.Tasks;
using Game.Landmarks.Model;
using Game.Others.Tools;
using Game.User;
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
            for (var i = 0; i < _reviewsContainer.childCount; i++)
            {
                Destroy(_reviewsContainer.GetChild(i).gameObject);
            }
            
            foreach (var review in landmarkModel.Reviews)
            {
                var reviewPrefab = Instantiate(_reviewPrefab, _reviewsContainer);
                var sprite = await imageLoader.LoadSpriteAsync(review.UserAvatar);
                reviewPrefab.SetData(review,sprite);
            }
        }

        public async void SendLocalReviews(ImageLoader imageLoader, UserModel userModel)
        {
            
            var reviewPrefab = Instantiate(_reviewPrefab, _reviewsContainer);
            var sprite = await imageLoader.LoadSpriteAsync(userModel.avatar);
            var reviews = new LandmarkReviews()
            {
                UserAvatar = userModel.avatar,
                UserName = userModel.name,
                Rating = (int)_starRating.GetRating(),
                Comment = _inputField.text
            };
            reviewPrefab.SetData(reviews,sprite);
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