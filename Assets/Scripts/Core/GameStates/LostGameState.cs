using Core.FMS;
using Game;
using UI;
using UI.Views;
using UnityEngine;

namespace Core
{
    public class LostGameState : IGameState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameLostScreenView _gameLostScreen;
        private readonly IGameController _gameController;

        public LostGameState(IGameStateMachine stateMachine, IAppContext appContext, IGameController gameController)
        {
            _stateMachine = stateMachine;
            _gameController = gameController;

            var uiManager = appContext.Resolve<IUImanager>();
            _gameLostScreen = uiManager.GetScreen<IGameLostScreenView>(ScreenType.GameLostScreen);

            _gameLostScreen.OnReplayGameButtonClick += OnReplayGameButtonClick;
        }

        public void Enter()
        {
            _gameLostScreen.Show();
            Time.timeScale = 0;
        }

        public void Exit()
        {
            _gameLostScreen.Hide();
            Time.timeScale = 1;
        }
        
        private void OnReplayGameButtonClick()
        {
            _stateMachine.SwitchState<PlayGameState>();
            _gameController.PlayGame();
        }
    }
}
