using Zenject;

namespace Game.User
{
    public class UserInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UserModel>().AsSingle();
        }
    }
}