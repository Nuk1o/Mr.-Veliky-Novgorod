using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UserServerService.Config;
using UniRx;
using UnityEngine;
using Zenject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserServerService.Data;

public class ServerController : IInitializable, IDisposable
{
    private readonly HttpClient _httpClient;
    private CompositeDisposable _compositeDisposable;

    [Inject]
    public ServerController()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri($"http://{ServerConfig.SERVER_ADRESS}")
        };
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ServerConfig.API_KEY}");
    }

    public void Initialize()
    {
        _compositeDisposable = new CompositeDisposable();

#if DEBUG_LOG
        Debug.Log($"ServerController initialized");
#endif
    }

    public void Dispose()
    {
        _compositeDisposable?.Dispose();
        _httpClient?.Dispose();
    }


    protected async UniTask<T> GetAsync<T>(string endpoint)
    {
        var cancellationTokenSource = new System.Threading.CancellationTokenSource();

        try
        {
            var response = await _httpClient.GetAsync($"{ServerConfig.SERVER_ADRESS}{endpoint}", cancellationTokenSource.Token);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(result);
            //var data = JsonUtility.FromJson<T>(result);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error fetching data: {e}");
            throw;
        }
    }

    protected async UniTask<ServerData> PostAsync<T>(string endpoint, T data)
    {
        try
        {
            var jsonData = JsonUtility.ToJson(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        
            Debug.Log($"Sending Request: {jsonData}");
        
            var response = await _httpClient.PostAsync($"{ServerConfig.SERVER_ADRESS}{endpoint}", content);
            response.EnsureSuccessStatusCode();
        
            var result = await response.Content.ReadAsStringAsync();
            Debug.Log($"Server Response: {result}");

            var jObject = JObject.Parse(result);
            var dataString = jObject["data"]?.ToString();
            
            var responseData = JsonUtility.FromJson<ServerData>(result);
            
            responseData.data = dataString;
            return responseData;
        }
        catch (HttpRequestException e)
        {
            Debug.LogError($"HTTP Error: {e.Message}");
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unexpected Error: {e}");
            throw;
        }
    }
}