using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Buildings
{
    public class BuildingUIView : UISampleView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _imageBuilding;
        [SerializeField] private TMP_Text _logoText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _historyText;
        
        public IObservable<Unit> CloseClickButton => _closeButton.OnClickAsObservable();

        public void SetDataBuilding(Sprite imageBuilding, string logo, string description, string history)
        {
            _imageBuilding.sprite = imageBuilding;
            _logoText.text = logo;
            _descriptionText.text = description;
            _historyText.text = history;
        }
    }
}