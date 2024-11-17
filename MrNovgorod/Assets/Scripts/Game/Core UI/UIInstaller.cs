using UnityEngine;
using Zenject;

namespace GameCore.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UISampleView _uiSampleView;
        [SerializeField] private ExampleHUDButtonView _exampleHUDButtonView;

        public override void InstallBindings()
        {
            Container.Bind<UISampleView>().FromInstance(_uiSampleView).AsSingle();

            Container.Bind<ExampleHUDButtonView>().FromInstance(_exampleHUDButtonView).AsSingle();

            Container.Bind<UISamplePresenter>().AsSingle()
                .WithArguments(_uiSampleView);

            Container.Bind<ExampleHUDButtonPresenter>().AsSingle()
                .WithArguments(_exampleHUDButtonView);

            Container.Bind<UINavigator>().AsSingle();
        }

        public override void Start()
        {
            Container.Resolve<UISamplePresenter>().Initialize();
            Container.Resolve<ExampleHUDButtonPresenter>().Initialize();
        }
    }
}