using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Game.Others.Tools
{
    public class ImageLoader : IInitializable, IDisposable
    {
        CompositeDisposable _disposable = new CompositeDisposable();
        public void Initialize()
        {
            _disposable = new CompositeDisposable();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        public async UniTask<Sprite> LoadSpriteAsync(string url)
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
    }
}