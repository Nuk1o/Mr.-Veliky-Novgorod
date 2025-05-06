using System;
using System.Collections.Generic;
using System.Linq;
using Game.Buildings;
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

        public void SetName(string name)
        {
            _title.text = name;
        }

        public void SetDescription(string description)
        {
            _description.text = description;
        }
        
        public void GenerateImages(Ebuildings buildingID, Dictionary<Ebuildings, List<Sprite>> dictionary)
        {
            DestroyAllImages();
            SetZeroPosition();

            foreach (var value in dictionary.Where(value => value.Key == buildingID))
            {
                foreach (var sprite in value.Value)
                {
                    var photo = Instantiate(_photoImagePrefab, Vector3.zero, Quaternion.identity, _photoContainer);
                    photo.sprite = sprite;
                }
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