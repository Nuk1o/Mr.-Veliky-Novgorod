using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Hud.ReviewsWindow;
using Game.Landmarks.Model;
using Game.User;
using Server.UserServerService.Data;
using UnityEngine;
using UserServerService;
using UserServerService.Data;
using UserServerService.Data.BuildingsData;
using Zenject;

namespace Server.UserServerService
{
    public class UserServerService : ServerController, IUserServerService
    {
        [Inject] private UserModel _userModel;
        public async UniTask<BuildingsServerData[]> GetBuildingsData()
        {
            var api = "attractions/get";

            try
            {
                var result = await GetAsync<BuildingsServerData[]>(api);
                return result;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loadig data: {e}");
            }

            return null;
        }

        public async UniTask RegisterUser(UserRegisterData userRegisterData)
        {
            var api = "register";
            
            // MyClass myStruct = new MyClass()
            // {
            //     name = "Aboba1",
            //     email = "abba1@gmail.com",
            //     password = "123",
            //     confirm_password = "123"
            // };

            try
            {
                var result = await PostAsync(api,userRegisterData);
                var responseData = JsonUtility.FromJson<ServerRegisterData>(result.data);
                Debug.Log($"Aboba {result} {responseData}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async UniTask<AuthorizationServerData> LoginUser(UserLoginData userLoginData)
        {
            var api = "login";
            
            // MyClass2 myStruct = new MyClass2()
            // {
            //     email = "abba1@gmail.com",
            //     password = "123",
            // };

            try
            {
                var result = await PostAsync(api,userLoginData);
                Debug.unityLogger.Log(result);
                var token = JsonUtility.FromJson<AuthorizationServerData>(result.data);
                _userModel.token = token.token;
                _userModel.IsLoggedIn = true;
                return token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async UniTask<ServerUserModel> GetUserData(string token)
        {
            var api = "profile";

            try
            {
                var headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {token}" }
                };

                var result = await GetAsync<ServerData>(api, headers);
                var userServerData = JsonUtility.FromJson<ServerUserModel>(result.data);
                _userModel.token = token;
                _userModel.IsLoggedIn = true;
                Debug.unityLogger.Log(result);
                return userServerData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async UniTask SendReview(LandmarkModel model, ReviewData data)
        {
            var api = $"attractions/{model.serverId}/reviews";

            try
            {
                ReviewServerData reviewServerData = new ReviewServerData()
                {
                    comment = data.comment,
                    rating = data.rating
                };
        
                var headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {_userModel.token}" }
                };
        
                var result = await PostAsync(api, reviewServerData, headers);
                Debug.Log(result);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}