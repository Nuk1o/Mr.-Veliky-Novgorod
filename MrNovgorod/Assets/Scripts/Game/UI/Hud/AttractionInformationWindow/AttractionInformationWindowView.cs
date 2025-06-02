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
        
        [SerializeField] private RectTransform _photoRectTransform;
        [SerializeField] private Image _photoImagePrefab;
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _history;

        [Header("Menu")]
        [SerializeField] private Button _mapButton;
        [SerializeField] private Button _coordButton;
        [SerializeField] private Button _review;
        
        public IObservable<Unit> CloseClickButton => _closeButton.OnClickAsObservable();
        
        public IObservable<Unit> MapClickButton => _mapButton.OnClickAsObservable();
        public IObservable<Unit> CoordClickButton => _coordButton.OnClickAsObservable();
        public IObservable<Unit> ReviewClickButton => _review.OnClickAsObservable();

        public void SetName(string nameBuilding)
        {
            _title.text = nameBuilding;
        }

        public void SetDescription(string description)
        {
            _description.text = description;
        }
        
        public void SetHistory(string history)
        {
            _history.text = history;
        }
        
        public void GenerateImages(Ebuildings buildingID, Dictionary<Ebuildings, List<Sprite>> dictionary)
        {
            DestroyAllImages();
            SetZeroPosition();

            foreach (var value in dictionary.Where(value => value.Key == buildingID))
            {
                foreach (var sprite in value.Value)
                {
                    var photo = Instantiate(_photoImagePrefab, Vector3.zero, Quaternion.identity, _photoRectTransform);
                    photo.sprite = sprite;
                }
            }
        }

        private void DestroyAllImages()
        {
            for (int index = 0; index < _photoRectTransform.childCount; index++)
            {
                Destroy(_photoRectTransform.GetChild(index).gameObject);
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