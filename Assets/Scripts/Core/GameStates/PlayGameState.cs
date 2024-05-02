using Core.FMS;
using Game;
using UI;
using UI.Views;

namespace Core
{
    public class PlayGameState : IGameState
    {
        private readonly IGameScreenView _gameScreen;
        private readonly IGameStateMachine _stateMachine;

        private readonly IGameController _gameController;

        private bool _firstPlay = true;
        
        public PlayGameState(IGameStateMachine stateMachine, IAppContext appContext, IGameController gameController)
        {
            _stateMachine = stateMachine;
            _gameController = gameController;

            _gameController.OnPlayerLost += OnPlayerLost;
            _gameController.OnPlayerWin += OnPlayerWin;

            var uiManager = appContext.Resolve<IUImanager>();

            _gameScreen = uiManager.GetScreen<IGameScreenView>(ScreenType.GameScreen);
            _gameScreen.OnPauseButtonClick += OnPauseButtonClick;
        }

        public void Enter()
        {
            _gameScreen.Show();
            if (_firstPlay)
            {
                _gameController.PlayGame();
                _firstPlay = false;
            }
        }

        public void Exit()
        {
            _gameScreen.Hide();
        }
        
        private void OnPlayerLost()
        {
            _stateMachine.SwitchState<LostGameState>();
        }

        private void OnPlayerWin()
        {
            _stateMachine.SwitchState<WinGameState>();
        }

        private void OnPauseButtonClick()
        {
            _stateMachine.SwitchState<PauseGameState>();
        }
    }
}