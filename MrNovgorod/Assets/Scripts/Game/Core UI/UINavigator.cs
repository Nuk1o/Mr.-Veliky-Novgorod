using Zenject;
using UnityEngine;

namespace GameCore.UI
{
    public class UINavigator
    {
        private readonly DiContainer _container;

        public UINavigator(DiContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Показывает окно через Presenter.
        /// </summary>
        public UINavigatorFlow<TPresenter, TView> Show<TPresenter, TView>()
            where TPresenter : UISystemPresenter<TView>
            where TView : UISystemView
        {
            var presenter = _container.Resolve<TPresenter>();
            return new UINavigatorFlow<TPresenter, TView>(presenter);
        }
    }

    public class UINavigatorFlow<TPresenter, TView>
        where TPresenter : UISystemPresenter<TView>
        where TView : UISystemView
    {
        private readonly TPresenter _presenter;

        public UINavigatorFlow(TPresenter presenter)
        {
            _presenter = presenter;
        }

        public UINavigatorFlow<TPresenter, TView> AsScreen()
        {
            Debug.Log($"[UINavigator] {typeof(TPresenter).Name} показан как Screen.");
            _presenter.Show();
            return this;
        }

        public UINavigatorFlow<TPresenter, TView> AsPopup()
        {
            Debug.Log($"[UINavigator] {typeof(TPresenter).Name} показан как Popup.");
            _presenter.Show();
            return this;
        }

        public UINavigatorFlow<TPresenter, TView> WithHUD()
        {
            Debug.Log($"[UINavigator] {typeof(TPresenter).Name} показан с HUD.");
            // Добавьте дополнительную логику для HUD.
            return this;
        }
    }
}