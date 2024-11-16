using UnityEngine;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log($"Instantiating SceneLoaderInstaller");
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
    }
}