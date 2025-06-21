using UnityEngine;
using Zenject;

namespace Game.UI.Popup
{
    public class PopupInstaller : MonoInstaller
    {
        [SerializeField] private PopupView _popupView;
        public override void InstallBindings()
        {
            Container.Bind<PopupView>().FromInstance(_popupView).AsSingle();
            Container.Bind<PopupPresenter>().AsSingle();
        }

        public override void Start()
        {
            Container.Resolve<PopupPresenter>().Setup(_popupView);
        }
    }
}