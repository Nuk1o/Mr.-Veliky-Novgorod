using System;
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
        
        public IObservable<Unit> CloseButtonClick => _exitButton.OnClickAsObservable();
        public IObservable<Unit> LoginButtonClick => _loginButton.OnClickAsObservable();
        public IObservable<Unit> RegisterButtonClick => _registerButton.OnClickAsObservable();
        
        public TMP_InputField NameInputField => _nameInputField;
        public TMP_InputField EmailInputField => _emailInputField;
        public TMP_InputField PasswordInputField => _passwordInputField;
    }
}