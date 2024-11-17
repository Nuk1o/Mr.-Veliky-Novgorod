using UnityEngine;

namespace GameCore.UI
{
    public abstract class UISystemView : MonoBehaviour
    {
        public abstract void Initialize();

        public virtual void BeforeShow() { }
        public virtual void AfterShow() { }
        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}