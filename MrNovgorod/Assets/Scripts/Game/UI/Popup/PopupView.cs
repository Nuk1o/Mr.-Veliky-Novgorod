using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopupView : MonoBehaviour
{
    [SerializeField] private GameObject _popup;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _popupTransform;
    
    private Tween _currentAnimation;
    private float _animationDuration = 0.6f;
    private float _displayDuration = 2f;
    
    private void Start()
    {
        _popup.SetActive(false);
        _canvasGroup.alpha = 0;
        _popupTransform.localScale = Vector3.zero;
    }

    public void ShowPopup(string title)
    {
        _currentAnimation?.Kill();
        
        _popup.SetActive(true);
        _title.text = title;
        
        var sequence = DOTween.Sequence();
        
        sequence.Append(_popupTransform.DOScale(1.2f, _animationDuration * 0.6f)
            .SetEase(Ease.OutBack));
        
        sequence.Join(_canvasGroup.DOFade(1, _animationDuration * 0.4f)
            .SetEase(Ease.InSine));
            
        sequence.Append(_popupTransform.DOShakePosition(_animationDuration * 0.3f, 15f, 10, 90, false, true)
            .SetEase(Ease.InOutSine));
            
        sequence.AppendInterval(_displayDuration);
        
        sequence.Append(_popupTransform.DOScale(0.8f, _animationDuration * 0.4f)
            .SetEase(Ease.InBack));
            
        sequence.Join(_canvasGroup.DOFade(0, _animationDuration * 0.6f)
            .SetEase(Ease.OutSine)
            .OnComplete(() => _popup.SetActive(false)));
            
        _currentAnimation = sequence;
    }
}