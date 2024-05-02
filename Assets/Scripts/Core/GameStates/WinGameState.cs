using Core.FMS;
using Game;
using UI;
using UI.Views;
using UnityEngine;

namespace Core
{
    public class WinGameState : IGameState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameWinScreenView _gameWinScreen;
        private readonly IGameController _gameController;

        public WinGameState(IGameStateMachine stateMachine, IAppContext appContext, IGameController gameController)
        {
            _stateMachine = stateMachine;
            _gameController = gameController;

            var uiManager = appContext.Resolve<IUImanager>();
            _gameWinScreen = uiManager.GetScreen<IGameWinScreenView>(ScreenType.GameWinScreen);

            _gameWinScreen.OnReplayButtonClick+= OnReplayButtonClick;
        }

        public void Enter()
        {
            _gameWinScreen.Show();
            Time.timeScale = 0;
        }

        public void Exit()
        {
            _gameWinScreen.Hide();
            Time.timeScale = 1;
        }
        
        private void OnReplayButtonClick()
        {
            _stateMachine.SwitchState<PlayGameState>();
            _gameController.PlayGame();
        }
    }
}
