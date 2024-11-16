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
            BaseAddress = new Uri($"http://{ServerConfig.SERVER_ADRESS}:{ServerConfig.SERVER_PORT}")
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

    /// <summary>
    /// Отправляет GET-запрос к API.
    /// </summary>
    /// <param name="endpoint">Конечная точка API.</param>
    /// <returns>Объект Observable для подписки.</returns>
    public IObservable<string> GetAsync(string endpoint)
    {
        return Observable.Create<string>(observer =>
        {
            var cancellationTokenSource = new System.Threading.CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    var response = await _httpClient.GetAsync(endpoint, cancellationTokenSource.Token);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    observer.OnNext(result);
                    observer.OnCompleted();
                }
                catch (Exception e)
                {
                    observer.OnError(e);
                }
            }, cancellationTokenSource.Token);

            return Disposable.Create(() => cancellationTokenSource.Cancel());
        });
    }

    /// <summary>
    /// Отправляет POST-запрос к API.
    /// </summary>
    /// <param name="endpoint">Конечная точка API.</param>
    /// <param name="data">Данные для отправки (объект будет сериализован в JSON).</param>
    /// <typeparam name="T">Тип данных для отправки.</typeparam>
    /// <returns>Объект Observable для подписки.</returns>
    public IObservable<string> PostAsync<T>(string endpoint, T data)
    {
        return Observable.Create<string>(observer =>
        {
            var cancellationTokenSource = new System.Threading.CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    var jsonData = JsonUtility.ToJson(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(endpoint, content, cancellationTokenSource.Token);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    observer.OnNext(result);
                    observer.OnCompleted();
                }
                catch (Exception e)
                {
                    observer.OnError(e);
                }
            }, cancellationTokenSource.Token);

            return Disposable.Create(() => cancellationTokenSource.Cancel());
        });
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