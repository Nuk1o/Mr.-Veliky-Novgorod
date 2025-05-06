using UnityEngine;
using Zenject;

namespace Game.Others.Tools
{
    public class ImageLoaderInstaller : MonoInstaller
    {
        [SerializeField] private BuildingsData _buildingsData;
        public override void InstallBindings()
        {
            Container.Bind<ImageLoader>().AsSingle();
        }
        
        public override void Start()
        {
            Container.Resolve<ImageLoader>().Initialize();
            Container.Resolve<ImageLoader>().SetupLocalData(_buildingsData);
            Container.Resolve<ImageLoader>().LoadSpriteAsync();
        }
    }
}