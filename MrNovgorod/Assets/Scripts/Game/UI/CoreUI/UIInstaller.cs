using Game.Hud;
using Game.Hud.AttractionInformationWindow;
using Game.UI.BuildingListController;
using UnityEngine;
using Zenject;

namespace GameCore.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private HUDCloseButtonView _hudCloseButtonView;
        [SerializeField] private AttractionInformationWindowView _attractionInformation;
        [SerializeField] private BuildingListView _buildingListView;

        public override void InstallBindings()
        {
            Container.Bind<HUDCloseButtonView>().FromInstance(_hudCloseButtonView).AsSingle();
            Container.Bind<HUDCloseButtonPresenter>().AsSingle() 
                .WithArguments(_hudCloseButtonView);

            Container.Bind<AttractionInformationWindowView>().FromInstance(_attractionInformation).AsSingle();
            Container.Bind<AttractionInformationWindowPresenter>().AsSingle();
            
            Container.Bind<BuildingListView>().FromInstance(_buildingListView).AsSingle();
            Container.Bind<BuildingListPresenter>().AsSingle();
            
            Container.Bind<UINavigator>().AsSingle().NonLazy();
        }

        public override void Start()
        {
            Container.Resolve<HUDCloseButtonPresenter>().Initialize();
            Container.Resolve<AttractionInformationWindowPresenter>().Initialize();
            Container.Resolve<BuildingListPresenter>().Initialize();
        }
    }
}