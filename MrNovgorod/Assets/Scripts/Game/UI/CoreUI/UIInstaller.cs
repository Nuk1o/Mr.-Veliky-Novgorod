using Game.Hud;
using Game.Hud.AttractionInformationWindow;
using UnityEngine;
using Zenject;

namespace GameCore.UI
{
    public class UIInstaller : MonoInstaller
    {
        // [SerializeField] private UISampleView _uiSampleView;
        // [SerializeField] private ExampleHUDButtonView _exampleHUDButtonView;
        [SerializeField] private HUDCloseButtonView _hudCloseButtonView;
        [SerializeField] private AttractionInformationWindowView _attractionInformation;

        public override void InstallBindings()
        {
            // Container.Bind<UISampleView>().FromInstance(_uiSampleView).AsSingle();
            // Container.Bind<UISamplePresenter>().AsSingle()
            //     .WithArguments(_uiSampleView);
            //
            // Container.Bind<ExampleHUDButtonView>().FromInstance(_exampleHUDButtonView).AsSingle();
            // Container.Bind<ExampleHUDButtonPresenter>().AsSingle()
            //     .WithArguments(_exampleHUDButtonView);

            Container.Bind<HUDCloseButtonView>().FromInstance(_hudCloseButtonView).AsSingle();
            Container.Bind<HUDCloseButtonPresenter>().AsSingle()
                .WithArguments(_hudCloseButtonView);

            Container.Bind<AttractionInformationWindowView>().FromInstance(_attractionInformation).AsSingle();
            Container.Bind<AttractionInformationWindowPresenter>().AsSingle()
                .WithArguments(_attractionInformation);
            
            Container.Bind<UINavigator>().AsSingle().NonLazy();
        }

        public override void Start()
        {
            // Container.Resolve<UISamplePresenter>().Initialize();
            // Container.Resolve<ExampleHUDButtonPresenter>().Initialize();
            Container.Resolve<HUDCloseButtonPresenter>().Initialize();
            Container.Resolve<AttractionInformationWindowPresenter>().Initialize();
        }
    }
}