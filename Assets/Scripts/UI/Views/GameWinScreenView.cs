using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class GameWinScreenView : BaseScreenView, IGameWinScreenView
    {
        [SerializeField] private Button _replayButtonButton;
        public override ScreenType Type => ScreenType.GameWinScreen;

        public event Action OnReplayButtonClick;

        protected override void OnAwake()
        {
            _replayButtonButton.onClick.AddListener(()=>
            {
                OnReplayButtonClick?.Invoke();
            });
        }
    }
}

