using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.BuildingListController
{
    public class SightView : MonoBehaviour
    {
        [SerializeField] private Button _openLandmarkButton;
        [SerializeField] private Image _imageLandmark;
        [SerializeField] private TMP_Text _textLandmark;

        public IObservable<Unit> OnOpenLandmarkAsObservable() => _openLandmarkButton.OnClickAsObservable();

        public void SetupLandmark(string landmarkName, Sprite landmarkSprite)
        {
            _textLandmark.text = landmarkName;
            _imageLandmark.sprite = landmarkSprite;
        }
    }
}