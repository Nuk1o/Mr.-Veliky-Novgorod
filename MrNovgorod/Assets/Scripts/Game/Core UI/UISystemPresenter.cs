using System;
using UnityEngine;
using Zenject;

namespace GameCore.UI
{
    public abstract class UISystemPresenter<TView> : IInitializable where TView : UISystemView
    {
        protected TView _view;
        
        public UISystemPresenter(TView view)
        {
            _view = view;
        }

        public void Show()
        {
            BeforeShow();
            _view.BeforeShow();
            _view.SetActive(true);
            AfterShow();
            _view.AfterShow();
        }

        protected virtual void BeforeShow() { }
        protected virtual void AfterShow() { }
        
        public virtual void Initialize() { }
    }
}