using System;
using UniRx;
using Zenject;

namespace Game.UI.Popup
{
    public class PopupPresenter : IInitializable, IDisposable
    {
        private CompositeDisposable _disposable;
        private PopupView _popupView;
        
        public void Initialize()
        {
            _disposable = new CompositeDisposable();
        }
        
        public void Setup(PopupView view)
        {
            Initialize();
            _popupView = view;
        }

        public void ShowPopup(string text)
        {
            _popupView.ShowPopup(text);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}