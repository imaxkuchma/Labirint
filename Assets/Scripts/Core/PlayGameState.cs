using System;
using UnityEngine;

public class PlayGameState : IGameState
{
    private IGameScreenView _gameScreen;
    private IGameStateMachine _stateMachine;

    private IGameController _gameController;

    private bool _firstRun = true;

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

    private void OnPlayerLost()
    {
        _stateMachine.SwitchState<LostGameState>();
    }

    private void OnPlayerWin()
    {
        _stateMachine.SwitchState<WinGameState>();
    }

    public void Enter()
    {
        _gameScreen.Show();
        if (_firstRun)
        {
            _gameController.PlayGame();
            _firstRun = false;
        }
    }

    public void Exit()
    {
        _gameScreen.Hide();
    }

    private void OnPauseButtonClick()
    {
        _stateMachine.SwitchState<PauseGameState>();
    }
}