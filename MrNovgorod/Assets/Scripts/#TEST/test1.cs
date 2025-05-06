using Server.ServerDataProviders;
using UnityEngine;

namespace _TEST
{
    public class test1:IServerDataProvider
    {
        public void Initialize()
        {
            Debug.Log($"ABOBA Initializing DataProvider 1");
        }
    }
}