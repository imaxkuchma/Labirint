using UnityEngine;

public class LostGameState : IGameState
{
    private IGameStateMachine _stateMachine;
    private IGameLostScreenView _gameLostScreen;
    private IGameController _gameController;

    public LostGameState(IGameStateMachine stateMachine, IAppContext appContext, IGameController gameController)
    {
        _stateMachine = stateMachine;
        _gameController = gameController;

        var uiManager = appContext.Resolve<IUImanager>();
        _gameLostScreen = uiManager.GetScreen<IGameLostScreenView>(ScreenType.GameLostScreen);

        _gameLostScreen.OnReplayGameButtonClick += OnReplayGameButtonClick;
    }

    private void OnReplayGameButtonClick()
    {
        _stateMachine.SwitchState<PlayGameState>();
        _gameController.PlayGame();
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
}
