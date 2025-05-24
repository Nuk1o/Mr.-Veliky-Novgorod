using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Game.User;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Views
{
    public class UIAccountView : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _registerButton;
        
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TMP_InputField _emailInputField;
        [SerializeField] private TMP_InputField _passwordInputField;
        
        [SerializeField] private SerializedDictionary<EAccountWindows,List<GameObject>> _uiAccountWindows;
        
        [Header("Profile")]
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _emailText;
        [SerializeField] private Image _avatarImage;
        
        public IObservable<Unit> CloseButtonClick => _exitButton.OnClickAsObservable();
        public IObservable<Unit> LoginButtonClick => _loginButton.OnClickAsObservable();
        public IObservable<Unit> RegisterButtonClick => _registerButton.OnClickAsObservable();
        
        public TMP_InputField NameInputField => _nameInputField;
        public TMP_InputField EmailInputField => _emailInputField;
        public TMP_InputField PasswordInputField => _passwordInputField;
        

        public void OpenWindow(EAccountWindows window)
        {
            foreach (var hideView in 
                     _uiAccountWindows.SelectMany(variable => variable.Value))
            {
                hideView.SetActive(false);
            }

            foreach (var view in _uiAccountWindows[window])
            {
                view.SetActive(true);
            }
        }

        public void SetDataProfile(ServerUserModel profileServerData, Sprite avatarImage)
        {
            _nameText.text = profileServerData.name;
            _emailText.text = profileServerData.email;
            if (avatarImage == null)
                return;
            _avatarImage.sprite = avatarImage;
        }
    }

    public enum EAccountWindows
    {
        Registration,
        Authorization,
        Profile,
    }
}