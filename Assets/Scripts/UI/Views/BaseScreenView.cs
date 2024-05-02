using UnityEngine;

namespace UI.Views
{
    public abstract class BaseScreenView : MonoBehaviour, IView
    {
        public abstract ScreenType Type { get; }

        private void Awake()
        {
            OnAwake();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnAwake()
        {

        }
    }
}
