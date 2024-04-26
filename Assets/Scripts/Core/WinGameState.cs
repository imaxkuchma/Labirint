using UnityEngine;

public class WinGameState : IGameState
{
    private IGameStateMachine _stateMachine;
    private IPlayerData _playerData;
    private IGameWinScreenView _gameWinScreen;
    private IGameController _gameController;

    public WinGameState(IGameStateMachine stateMachine, IAppContext appContext, IGameController gameController)
    {
        _stateMachine = stateMachine;
        _playerData = appContext.Resolve<IPlayerData>();
        _gameController = gameController;

        var uiManager = appContext.Resolve<IUImanager>();
        _gameWinScreen = uiManager.GetScreen<IGameWinScreenView>(ScreenType.GameWinScreen);

        _gameWinScreen.OnReplayButtonClick+= OnReplayButtonClick;
    }

    private void OnReplayButtonClick()
    {
        _stateMachine.SwitchState<PlayGameState>();
        _gameController.PlayGame();
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
}
