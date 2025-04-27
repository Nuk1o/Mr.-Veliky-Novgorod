using System;
using Zenject;

namespace Game.landmarks.Installer
{
    public class LandmarksInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LandmarksPresenter>().AsSingle();
        }

        private void Awake()
        {
            Container.Resolve<LandmarksPresenter>().Initialize();
        }
    }
}