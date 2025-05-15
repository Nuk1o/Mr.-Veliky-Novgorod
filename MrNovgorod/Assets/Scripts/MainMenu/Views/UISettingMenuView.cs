using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Views
{
    public class UISettingMenuView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [Space]
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private Slider _slider;
        [Space]
        [SerializeField] private Button _closeButton;
        
        public IObservable<Unit> CloseButtonClick => _closeButton.OnClickAsObservable();

        public void LoadScreen()
        {
            _title.text = "Settings";
            _slider.value = 0;
        }
    }
}