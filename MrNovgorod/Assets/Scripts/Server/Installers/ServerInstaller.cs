using System;
using System.Linq;
using System.Reflection;
using Server.ServerDataProviders;
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
            
            //Container.BindInterfacesAndSelfTo<IServerDataProvider>();
            
            // Assembly assembly = Assembly.GetExecutingAssembly();
            // Type interfaceType = typeof(IServerDataProvider);
            //
            // var implementingTypes = assembly.GetTypes()
            //     .Where(type => type.IsClass && interfaceType.IsAssignableFrom(type));
            //
            // Debug.Log($"Классы, реализующие интерфейс {interfaceType.Name}:");
            // foreach (var type in implementingTypes)
            // {
            //     Debug.Log($"ABOBA - {type.Name}");
            // }
            
        }
    }
}