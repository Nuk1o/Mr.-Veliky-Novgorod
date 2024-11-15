using MainMenu.Presenters;
using MainMenu.Views;
using UnityEngine;
using Zenject;

namespace MainMenu.Installers
{
    public class UIMainMenuInstaller : MonoInstaller
    {
        [SerializeField] private UIMainMenuView _uiMainMenuView;
        [SerializeField] private UISettingMenuView _uiSettingMenuView;

        public override void InstallBindings()
        {
            Container.Bind<UIMainMenuView>().FromInstance(_uiMainMenuView).AsSingle();
            
            Container.Bind<UISettingMenuView>().FromInstance(_uiSettingMenuView).AsSingle();
            
            Container.Bind<UIMainMenuPresenter>().AsSingle()
                .WithArguments(_uiMainMenuView);

            Container.Bind<UISettingMenuPresenter>().AsSingle()
                .WithArguments(_uiSettingMenuView);
        }


        public override void Start()
        {
            Container.Resolve<UIMainMenuPresenter>().Initialize();
            Container.Resolve<UISettingMenuPresenter>().Initialize();
        }
    }
}