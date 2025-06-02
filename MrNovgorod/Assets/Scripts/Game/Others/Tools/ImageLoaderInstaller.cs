using Zenject;

namespace Game.Others.Tools
{
    public class ImageLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ImageLoader>().AsSingle();
        }
        
        public override void Start()
        {
            Container.Resolve<ImageLoader>().Initialize();
        }
    }
}