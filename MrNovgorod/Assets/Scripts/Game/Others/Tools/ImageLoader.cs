using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class ImageLoader : IInitializable, IDisposable
{
    List<Sprite> _images = new List<Sprite>();
    List<string> _urls = new List<string>()
    {
        "http://kalotun123.beget.tech/storage/images/novgorod_kremlin.jpg",
        "http://kalotun123.beget.tech/storage/images/Vitoslavlitsy.jpg"
    };

    CompositeDisposable _disposable = new CompositeDisposable();

    public void Initialize()
    {
        _disposable = new CompositeDisposable();
        LoadSpriteAsync();
    }

    public List<Sprite> GetSprites()
    {
        return _images;
    }

    private async void LoadSpriteAsync()
    {
        var list = await LoadImage(_urls);
        _images = list;
        Debug.Log($"Aboba {list.Count} images");
    }

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