using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
    }
}