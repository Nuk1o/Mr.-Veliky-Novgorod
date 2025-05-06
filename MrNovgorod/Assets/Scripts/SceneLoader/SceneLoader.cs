using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : IInitializable, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    public ReactiveProperty<float> LoadProgress { get; private set; } = new ReactiveProperty<float>();
    public ReactiveProperty<bool> IsLoading { get; private set; } = new ReactiveProperty<bool>(false);

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }

    public IObservable<Unit> LoadSceneAsync(string sceneName)
    {
        if (IsLoading.Value)
        {
            return Observable.ReturnUnit();
        }

        IsLoading.Value = true;
        LoadProgress.Value = 0f;
        
        return Observable.FromCoroutine<Unit>(_ => LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            LoadProgress.Value = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
        
        LoadProgress.Value = 1f;
        IsLoading.Value = false;
    }
}