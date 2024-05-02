using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class GameLostScreenView : BaseScreenView, IGameLostScreenView
    {    
        [SerializeField] private Button _repeatLevelButton;
        public override ScreenType Type => ScreenType.GameLostScreen;

        public event Action OnReplayGameButtonClick;

        protected override void OnAwake()
        {
            _repeatLevelButton.onClick.AddListener(() => 
            {
                OnReplayGameButtonClick?.Invoke();
            });
        }
    }
}

