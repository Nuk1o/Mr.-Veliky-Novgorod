using Cysharp.Threading.Tasks;
using Game.User;
using Server.UserServerService.Data;
using UserServerService;
using UserServerService.Data.BuildingsData;

namespace Server.UserServerService
{
    public interface IUserServerService
    {
        public UniTask<BuildingsServerData[]> GetBuildingsData();
        public UniTask RegisterUser(UserRegisterData userRegisterData);
        public UniTask<AuthorizationServerData> LoginUser(UserLoginData userLoginData);
        public UniTask<ServerUserModel> GetUserData(string token);
    }
}