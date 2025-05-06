using GameCore.UI;
using UniRx;
using UnityEngine;
using Zenject;

public class ExampleHUDButtonPresenter : UISystemPresenter<ExampleHUDButtonView>
{
    [Inject] private UINavigator _uiNavigator;

    private ExampleHUDButtonView _view;
    private CompositeDisposable _disposable;

    public void OpenScreen()
    {
        _uiNavigator.Show<UISamplePresenter, UISampleView>().AsScreen().WithHUD();
    }
    //
    // public void OpenPopupWithHUD()
    // {
    //     _uiNavigator.Show<UISamplePresenter, UISampleView>().AsPopup();
    // }

    // public override void Initialize()
    // {
    //     base.Initialize();
    //     Debug.Log("ExampleHUDButtonPresenter: Initialize");
    // }
    //
    // public override void Dispose()
    // {
    //     base.Dispose();
    //     Debug.Log("ExampleHUDButtonPresenter: Dispose");
    // }

    public ExampleHUDButtonPresenter(ExampleHUDButtonView view) : base(view)
    {
        _view = view;
    }

    public void Initialize()
    {
        _disposable = new CompositeDisposable();
        Debug.Log("ExampleHUDButtonPresenter: Initialize");
        _view.Initialize();
        _view.TestClickButton
            .Subscribe(_ => OpenScreen())
            .AddTo(_disposable);
    }
}