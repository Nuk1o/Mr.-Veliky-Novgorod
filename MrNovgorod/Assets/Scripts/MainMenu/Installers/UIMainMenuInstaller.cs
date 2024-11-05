using MainMenu.Presenters;
using MainMenu.Views;
using UnityEngine;
using Zenject;

namespace MainMenu.Installers
{
    public class UIMainMenuInstaller : MonoInstaller
    {
        [SerializeField] private UIMainMenuView _uiMainMenuView;
        private UIMainMenuPresenter _presenter;

        public override void InstallBindings()
        {
            Container.Bind<UIMainMenuView>().FromInstance(_uiMainMenuView).AsSingle();
            Container.Bind<UIMainMenuPresenter>().AsSingle().WithArguments(_uiMainMenuView);
        }

        public override void Start()
        {
            _presenter = Container.Resolve<UIMainMenuPresenter>();
            _presenter.Initialize();
        }
    }
}