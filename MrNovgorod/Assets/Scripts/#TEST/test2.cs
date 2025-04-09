using Server.ServerDataProviders;
using UnityEngine;

namespace _TEST
{
    public class test2:IServerDataProvider
    {
        public void Initialize()
        {
            Debug.Log($"ABOBA Initializing DataProvider 2");
        }
    }
}