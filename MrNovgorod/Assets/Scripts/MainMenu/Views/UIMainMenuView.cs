using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Views
{
    [Serializable]
    public class UIMainMenuView : MonoBehaviour
    {
        [Header("Logo")]
        [SerializeField] private TMP_Text _logo;
        [Space]
        [Header("Buttons")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _exitGameButton;

        public IObservable<Unit> StartClickButton => _startGameButton.OnClickAsObservable();
        public IObservable<Unit> SettingClickButton => _settingButton.OnClickAsObservable();
        public IObservable<Unit> ExitClickButton => _exitGameButton.OnClickAsObservable();
    }
}