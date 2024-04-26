using UnityEngine;

public class PauseGameState : IGameState
{
    private IMenuScreenView _menuScreen;
    private IGameStateMachine _stateMachine;
    private IPlayerData _playerData;
    private IGameController _gameController;
    public PauseGameState(IGameStateMachine stateMachine, IAppContext appContext, IGameController gameController)
    {
        _stateMachine = stateMachine;
        _playerData = appContext.Resolve<IPlayerData>();
        _gameController = gameController;

        var uiManager = appContext.Resolve<IUImanager>();
        _menuScreen = uiManager.GetScreen<IMenuScreenView>(ScreenType.MenuScreen);

        _menuScreen.OnLoadButtonClick += OnLoadButtonClick;
        _menuScreen.OnSaveButtonClick += OnSaveButtonClick;
    }

    private void OnLoadButtonClick()
    {  
        _stateMachine.SwitchState<PlayGameState>();
        _playerData.Load();
        _gameController.PlayGame(true);
    }

    private void OnSaveButtonClick()
    {
        _playerData.SetTimeleft(_gameController.TimeLeft);
        _playerData.SetAttemptCount(_gameController.AttemptCount);
        _playerData.Save();    
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
