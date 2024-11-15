using Server.ServerDataProviders.UserServerDataProvider;
using UnityEngine;
using Zenject;

namespace Server.Installers
{
    public class ServerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if DEBUG_LOG
            Debug.Log($"Server installer started");
#endif
            Container.BindInterfacesAndSelfTo<ServerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<UserServerDataProvider>().AsSingle();
        }
    }
}