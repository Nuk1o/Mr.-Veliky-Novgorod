using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Hud
{
    public class HUDCloseButtonView : MonoBehaviour

    {
    [SerializeField] Button _closeButton;

    public IObservable<Unit> CloseClickButton => _closeButton.OnClickAsObservable();
    }
}