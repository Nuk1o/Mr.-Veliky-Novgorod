using MainMenu.Presenters;
using MainMenu.Views;
using UnityEngine;
using Zenject;

namespace MainMenu.Installers
{
    public class UIMainMenuInstaller : MonoInstaller
    {
        [SerializeField] private UIMainMenuView _uiMainMenuView;
        [SerializeField] private UIAccountView _uiAccountView;

        public override void InstallBindings()
        {
            Container.Bind<UIMainMenuView>().FromInstance(_uiMainMenuView).AsSingle();
            Container.Bind<UIAccountView>().FromInstance(_uiAccountView).AsSingle();
            
            Container.Bind<UIMainMenuPresenter>().AsSingle()
                .WithArguments(_uiMainMenuView);
            
            Container.Bind<UIAccountPresenter>().AsSingle()
                .WithArguments(_uiAccountView);
        }

        public override void Start()
        {
            Container.Resolve<UIMainMenuPresenter>().Initialize();
            Container.Resolve<UIAccountPresenter>().Initialize();
        }
    }
}