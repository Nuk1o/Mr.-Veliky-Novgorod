using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.BuildingListController
{
    public class BuildingListView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Button _antiClickButton;
        [SerializeField] private Transform _content;
        [SerializeField] private SightView _sightViewPrefab;
        [SerializeField] private GameObject _menu;
        
        public IObservable<Unit> MenuClickButton => _button.OnClickAsObservable().Merge(_antiClickButton.OnClickAsObservable());
        public bool MenuIsActive => _menu.activeSelf;

        public SightView SpawnSightObject()
        {
            var sight = Instantiate(_sightViewPrefab,_content);
            return sight;
        }

        public void SetActiveMenu(bool active)
        {
            _menu.SetActive(active);
        }
    }
}