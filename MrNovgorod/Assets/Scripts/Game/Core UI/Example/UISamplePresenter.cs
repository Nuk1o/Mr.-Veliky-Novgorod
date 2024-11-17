using GameCore.UI;
using UnityEngine;

public class UISamplePresenter : UISystemPresenter<UISampleView>
{
    public UISamplePresenter(UISampleView view) : base(view)
    {
        _view = view;
    }
    
    protected override void BeforeShow()
    {
        Debug.Log("UISamplePresenter: BeforeShow");
    }

    protected override void AfterShow()
    {
        Debug.Log("UISamplePresenter: AfterShow");
    }

    public void Initialize()
    {
        Debug.Log("UISamplePresenter: Initialize");
    }
}