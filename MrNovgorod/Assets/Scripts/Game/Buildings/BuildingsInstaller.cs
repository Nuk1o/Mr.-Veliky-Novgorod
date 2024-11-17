using Game.Buildings.Pins;
using UnityEngine;
using Zenject;

namespace Game.Buildings
{
    public class BuildingsInstaller : MonoInstaller
    {
        [SerializeField] private PinUIView[] _pinUIViews;
        [SerializeField] private BuildingUIView _buildingUIView;

        public override void InstallBindings()
        {
            for (int i = 0; i < _pinUIViews.Length; i++)
            {
                var pinUIView = _pinUIViews[i];
                Container.Bind<PinUIView>().FromInstance(pinUIView).AsTransient();
                Container.Bind<PinUIPresenter>().WithId(i).AsTransient()
                    .WithArguments(pinUIView);
            }

            Container.Bind<BuildingUIView>().FromInstance(_buildingUIView).AsSingle();
            Container.Bind<BuildingUIPresenter>().AsSingle()
                .WithArguments(_buildingUIView);
        }

        public override void Start()
        {
            // Resolve each PinUIPresenter by its unique identifier
            for (int i = 0; i < _pinUIViews.Length; i++)
            {
                var presenter = Container.ResolveId<PinUIPresenter>(i);
                presenter.Initialize();
            }

            Container.Resolve<BuildingUIPresenter>().Initialize();
        }
    }
}