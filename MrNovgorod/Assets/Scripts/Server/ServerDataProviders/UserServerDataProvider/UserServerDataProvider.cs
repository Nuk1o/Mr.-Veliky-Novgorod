using System;
using System.Threading.Tasks;
using Server.Data;
using Server.Data.User;
using UniRx;
using UnityEngine;

namespace Server.ServerDataProviders.UserServerDataProvider
{
    public class UserServerDataProvider : ServerController
    {
        public async Task FetchData(CompositeDisposable disposable)
        {
            var endpoint = "/api/example";

            try
            {
                var result = await GetAsync<UserServerData>(endpoint);
                Debug.Log($"Data fetched: ID = {result.id}, Name = {result.userName}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error fetching data: {e}");
            }
        }

        public async Task TestLogin(CompositeDisposable disposable)
        {
            var endpoint = "/api/test";

            try
            {
                var result = await PostAsync(endpoint, new ServerData());
                Debug.Log($"Data fetched: result = {result.result}, Name = {result}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error fetching data: {e}");
            }
        }

    }
}