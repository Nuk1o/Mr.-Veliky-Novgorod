using System;
using System.Collections.Generic;
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
using UnityEngine.Networking;
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
    
            var responseData = JsonUtility.FromJson<ServerData>(result);
            
            responseData.data = result;
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

    protected async UniTask<ServerData> GetAsync<T>(
        string endpoint,
        Dictionary<string, string> headers = null)
    {
        var url = $"{ServerConfig.SERVER_ADRESS}{endpoint}";

        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }

            request.uploadHandler = new UploadHandlerRaw(new byte[0]);
            request.downloadHandler = new DownloadHandlerBuffer();

            Debug.Log($"Sending empty POST request to {url}");

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                string error = $"HTTP error {request.responseCode}: {request.error}";
                Debug.LogError(error);
                throw new Exception(error);
            }

            string result = request.downloadHandler.text;
            Debug.Log($"Server Response: {result}");

            try
            {
                var resultRequest = JsonUtility.FromJson<ServerData>(result);
                resultRequest.data = result;
                return resultRequest;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to deserialize response: {e.Message}");
                throw;
            }
        }
    }
}