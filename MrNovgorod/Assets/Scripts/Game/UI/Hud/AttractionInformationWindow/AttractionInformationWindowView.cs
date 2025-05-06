using System;
using System.Collections.Generic;
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
        [SerializeField] private RectTransform _photoRectTransform;
        [SerializeField] private Image _photoImagePrefab;
        
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
        
        public void GenerateImages(List<Sprite> sprites)
        {
            DestroyAllImages();
            SetZeroPosition();
            
            foreach (var sprite in sprites)
            {
                var photo = Instantiate(_photoImagePrefab, Vector3.zero, Quaternion.identity, _photoContainer);
                photo.sprite = sprite;
            }
        }

        private void DestroyAllImages()
        {
            for (int index = 0; index < _photoContainer.childCount; index++)
            {
                Destroy(_photoContainer.GetChild(index).gameObject);
            }
        }

        private void SetZeroPosition()
        {
            _photoRectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}