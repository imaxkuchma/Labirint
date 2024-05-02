using Core.FMS;
using Data;
using Game;
using UI;
using UI.Views;
using UnityEngine;

namespace Core
{
    public class PauseGameState : IGameState
    {
        private readonly IMenuScreenView _menuScreen;
        private readonly IGameStateMachine _stateMachine;
        private readonly ILevelData _levelData;
        private readonly IGameController _gameController;

        public PauseGameState(IGameStateMachine stateMachine, IAppContext appContext, IGameController gameController)
        {
            _stateMachine = stateMachine;
            _levelData = appContext.Resolve<ILevelData>();
            _gameController = gameController;

            var uiManager = appContext.Resolve<IUImanager>();
            _menuScreen = uiManager.GetScreen<IMenuScreenView>(ScreenType.MenuScreen);

            _menuScreen.OnLoadButtonClick += OnLoadButtonClick;
            _menuScreen.OnSaveButtonClick += OnSaveButtonClick;
        }

        private void OnLoadButtonClick()
        {  
            _stateMachine.SwitchState<PlayGameState>();
            _levelData.Load();
            _gameController.PlayGame(loadData:true, isResume:false);
        }

        private void OnSaveButtonClick()
        {
            _levelData.SetTimeLeft(_gameController.TimeLeft);
            _levelData.SetAttemptCount(_gameController.AttemptCount);
            _levelData.SetPlayerPosition(_gameController.GetPlayerPosition());
            _levelData.SetEmeniesPosition(_gameController.GetEnemiesPositions());
            _levelData.Save(); 
            
            _stateMachine.SwitchState<PlayGameState>();
            _gameController.PlayGame(loadData:false, isResume:true);
        }

        public void Enter()
        {
            _menuScreen.Show();
            Time.timeScale = 0;
        }

        public void Exit()
        {
            _menuScreen.Hide();
            Time.timeScale = 1;
        }
    }
}
