using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Buildings;
using Game.Landmarks.Model;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class LandmarksImageLoader : IInitializable, IDisposable
{
    [Inject] private LandmarksModel _landmarksModel;
    private BuildingsData _landmarksLocalModel;
    private Dictionary<Ebuildings, List<Sprite>> _images;

    private CompositeDisposable _disposable;

    public void Initialize()
    {
        _disposable = new CompositeDisposable();
        _images = new Dictionary<Ebuildings, List<Sprite>>();
    }

    public void SetupLocalData(BuildingsData landmarksLocalModel)
    {
        _landmarksLocalModel = landmarksLocalModel;
    }

    public Dictionary<Ebuildings, List<Sprite>> GetSprites()
    {
        return _images;
    }

    public async void LoadSpriteAsync()
    {
#if SERVER_ON
        foreach (var landmarkModel in _landmarksModel.Buildings)
        {
            var list = await LoadImage(landmarkModel.Value.ImageUrls);
            var spriteList = list.ToList();
            _images.Add(landmarkModel.Key, spriteList);
        }
    }
#else
        foreach (var landmarkModel in _landmarksLocalModel.Buildings)
        {
            var list = await LoadImage(landmarkModel.Value.ImageUrls);
            var spriteList = list.ToList();
            _images.Add(landmarkModel.Key, spriteList);
        }
    }
#endif

    private async UniTask<List<Sprite>> LoadImage(List<string> urls)
    {
        var sprites = new List<Sprite>();

        foreach (var url in urls)
        {
            var sprite = await LoadSpriteAsync(url);
            sprites.Add(sprite);
        }

        return new List<Sprite>(sprites);
    }

    private async UniTask<Sprite> LoadSpriteAsync(string url)
    {
        using var www = UnityWebRequestTexture.GetTexture(url);

        try
        {
            await www.SendWebRequest().ToUniTask();

            if (www.result != UnityWebRequest.Result.Success)
                throw new Exception($"Error loading {url}: {www.error}");

            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            return Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.zero);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load sprite from {url}: {ex.Message}");
            throw;
        }
    }

    public void Dispose()
    {
        _disposable?.Dispose();
    }
}