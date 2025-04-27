using System;
using GameCore.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ExampleHUDButtonView : UISystemView
{
    [SerializeField] private Button _button;

    public IObservable<Unit> TestClickButton => _button.OnClickAsObservable();

    public override void Initialize()
    {
        Debug.Log("ExampleHUDButtonView: Initialize");
    }

    public override void BeforeShow()
    {
        Debug.Log("ExampleHUDButtonView: BeforeShow");
    }

    public override void AfterShow()
    {
        Debug.Log("ExampleHUDButtonView: AfterShow");
    }
}