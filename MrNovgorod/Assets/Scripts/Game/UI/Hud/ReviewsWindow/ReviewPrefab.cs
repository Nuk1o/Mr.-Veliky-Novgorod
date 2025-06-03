using Game.Landmarks.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Hud.ReviewsWindow
{
    public class ReviewPrefab : MonoBehaviour
    {
        [SerializeField] private Image _userAvatar;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _reviewText;
        [SerializeField] private TMP_Text _ratingText;

        public void SetData(LandmarkReviews review, Sprite sprite)
        {
            _userAvatar.sprite = sprite;
            _nameText.text = review.UserName;
            _reviewText.text = review.Comment;
            _ratingText.text = $"Оценка: {review.Rating}";
        }
    }
}