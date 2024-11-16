using UniRx;
using UnityEngine;

namespace Server.ServerDataProviders.UserServerDataProvider
{
    public class UserServerDataProvider : ServerController
    {
        public void FetchData(CompositeDisposable disposable)
        {
            var endpoint = "/api/example";
        
            GetAsync(endpoint)
                .Subscribe(
                    result => Debug.Log($"Data fetched: {result}"),
                    error => Debug.LogError($"Error fetching data: {error}")
                )
                .AddTo(disposable);
        }
    }
}