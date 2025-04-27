using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Buildings.Pins
{
    public class PinUIView : MonoBehaviour
    {
        [SerializeField] private Button _buttonPin;
        
        public BuildingData BuildingData;
        
        public IObservable<Unit> PinClickButton => _buttonPin.OnClickAsObservable();
    }
}