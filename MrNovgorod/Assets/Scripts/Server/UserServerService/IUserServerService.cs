using Cysharp.Threading.Tasks;
using Server.UserServerService.Data;
using UserServerService.Data.BuildingsData;

namespace Server.UserServerService
{
    public interface IUserServerService
    {
        public UniTask<BuildingsServerData[]> GetBuildingsData();
        public UniTask RegisterUser(UserRegisterData userRegisterData);
        public UniTask LoginUser(UserLoginData userLoginData);
        public UniTask GetUserData(string token);
    }
}