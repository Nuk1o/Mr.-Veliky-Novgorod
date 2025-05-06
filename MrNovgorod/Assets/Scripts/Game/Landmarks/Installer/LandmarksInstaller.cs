using Game.Landmarks.DataProvider;
using Game.Landmarks.Model;
using Zenject;

namespace Game.Landmarks.Installer
{
    public class LandmarksInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LandmarksModel>().AsSingle();
            Container.Bind<LandmarksDataProvider>().AsSingle();
        }
        
        public override void Start()
        {
            Container.Resolve<LandmarksDataProvider>().Initialize();
        }
    }
}