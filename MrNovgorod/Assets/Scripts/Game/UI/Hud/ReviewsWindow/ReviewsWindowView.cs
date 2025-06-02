using System;
using GameCore.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Hud.ReviewsWindow
{
    public class ReviewsWindowView : UISystemView
    {
        [SerializeField] private Button _sendButton;
        [SerializeField] private Button _backButton;
        
        public IObservable<Unit> SendClickButton => _sendButton.OnClickAsObservable();
        public IObservable<Unit> BackClickButton => _backButton.OnClickAsObservable();
        
        public override void Initialize()
        {
            
        }
    }
}