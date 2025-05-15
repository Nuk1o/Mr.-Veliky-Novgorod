using UnityEngine;
using Zenject;

namespace UserServerService.Installers
{
    public class ServerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if DEBUG_LOG
            Debug.Log($"Server installer started");
#endif
            Container.BindInterfacesAndSelfTo<ServerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<Server.UserServerService.UserServerService>().AsSingle();
            
        }
    }
}