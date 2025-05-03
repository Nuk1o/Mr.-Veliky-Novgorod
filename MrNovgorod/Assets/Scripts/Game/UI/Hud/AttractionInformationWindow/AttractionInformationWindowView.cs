using System;
using GameCore.UI;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Hud.AttractionInformationWindow
{
    public class AttractionInformationWindowView : UISystemView
    {
        [SerializeField] private Button _closeButton;
        
        [SerializeField] private Transform _photoContainer;
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        
        public IObservable<Unit> CloseClickButton => _closeButton.OnClickAsObservable();
        
        public Transform PhotoContainer => _photoContainer;

        public void SetName(string name)
        {
            _title.text = name;
        }

        public void SetDescription(string description)
        {
            _description.text = description;
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}