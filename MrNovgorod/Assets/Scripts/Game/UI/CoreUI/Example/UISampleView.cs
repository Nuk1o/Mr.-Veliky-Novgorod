using GameCore.UI;
using UnityEngine;

public class UISampleView : UISystemView
{
    public override void Initialize()
    {
        Debug.Log("UISampleView: Initialize");
    }

    public override void BeforeShow()
    {
        Debug.Log("UISampleView: BeforeShow");
    }

    public override void AfterShow()
    {
        Debug.Log("UISampleView: AfterShow");
    }
}