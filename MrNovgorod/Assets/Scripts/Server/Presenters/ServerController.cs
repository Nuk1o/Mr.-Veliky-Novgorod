using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Server.Config;
using UniRx;
using UnityEngine;
using Zenject;

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

    
    public async Task<T> GetAsync<T>(string endpoint)
    {
        var cancellationTokenSource = new System.Threading.CancellationTokenSource();

        try
        {
            var response = await _httpClient.GetAsync(endpoint, cancellationTokenSource.Token);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonUtility.FromJson<T>(result); // Десериализация
            return data;
        }
        catch (Exception e)
        {
            // Обработка ошибок (можно выбросить исключение или вернуть null)
            Debug.LogError($"Error fetching data: {e}");
            throw; // Или return null; в зависимости от вашей логики
        }
    }

    public async Task<T> PostAsync<T>(string endpoint, T data)
    {
        var cancellationTokenSource = new System.Threading.CancellationTokenSource();

        try
        {
            var jsonData = JsonUtility.ToJson(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{ServerConfig.SERVER_ADRESS}{endpoint}", content, cancellationTokenSource.Token);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var responseData = JsonUtility.FromJson<T>(result); 
            return responseData;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error fetching data: {e}");
            throw;
        }
    }


    /// <summary>
    /// Пример обработки запроса и обработки результата.
    /// </summary>
    // public void FetchData()
    // {
    //     var endpoint = "/api/example";
    //     
    //     GetAsync(endpoint)
    //         .Subscribe(
    //             result => Debug.Log($"Data fetched: {result}"),
    //             error => Debug.LogError($"Error fetching data: {error}")
    //         )
    //         .AddTo(_compositeDisposable);
    // }
}