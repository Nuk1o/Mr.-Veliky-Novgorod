using UnityEngine;
using Zenject;

namespace Game.Others.Tools
{
    public class LandmarksImageLoaderInstaller : MonoInstaller
    {
        [SerializeField] private BuildingsData _buildingsData;
        public override void InstallBindings()
        {
            Container.Bind<LandmarksImageLoader>().AsSingle();
        }
        
        public override void Start()
        {
            Container.Resolve<LandmarksImageLoader>().Initialize();
            Container.Resolve<LandmarksImageLoader>().SetupLocalData(_buildingsData);
            Container.Resolve<LandmarksImageLoader>().LoadSpriteAsync();
        }
    }
}