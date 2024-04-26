using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public interface IGameController
{
    event Action OnPlayerWin;
    event Action OnPlayerLost;
    int TimeLeft { get; }
    int AttemptCount { get; }
    void PlayGame(bool loadData = false);
}

public class GameController : IGameController
{
    private IPlayerData _playerData;
    private ICameraController _cameraController;
    private Level _levelPrefab;
    private PlayerController _playerPrefab;
    private IGameScreenView _gameScreen;

    private Level _level;
    private PlayerController _player;

    public event Action OnPlayerWin;
    public event Action OnPlayerLost;

    private TimeCounter _timeCounter;

    private const int _levelTimeLeft = 30;

    public int TimeLeft { get; private set; }
    public int AttemptCount { get; private set; } = 0;

    CancellationTokenSource cancellationTokenSource;

    public GameController(IAppContext appContext)
    {
        _playerData = appContext.Resolve<IPlayerData>();
        _levelPrefab = appContext.Resolve<Level>();
        _playerPrefab = appContext.Resolve<PlayerController>();
        _cameraController = appContext.Resolve<ICameraController>();

        var uiManager = appContext.Resolve<IUImanager>();
        _gameScreen = uiManager.GetScreen<IGameScreenView>(ScreenType.GameScreen);

        _timeCounter = new TimeCounter();
        _timeCounter.OnTimeChange += OnTimeChange;

        Events.OnOnPlayerDamaged += OnOnPlayerDamaged;
    }

    private void OnOnPlayerDamaged()
    {
        OnPlayerLost?.Invoke();
    }

    private void OnTimeChange(int time)
    {
        TimeLeft = time;

        _gameScreen.SetTimeleft(time);

        if (time == 0)
        {
            OnPlayerLost?.Invoke();
        }    
    }

    public void PlayGame(bool loadData = false)
    {

        if (!loadData)
        {
            AttemptCount++;
        }
        else
        {
            AttemptCount = _playerData.AttemptCount + 1;
        }

        _gameScreen.SetNumberAttempts(AttemptCount);


        if (_level != null)
        {
            GameObject.Destroy(_level.gameObject);
        }
        if (_player != null)
        {
            GameObject.Destroy(_player.gameObject);
        }

        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
        }

        _level = GameObject.Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _level.PlayerFinishPoint.OnPlayerFinished += () => OnPlayerWin?.Invoke();

        _player = GameObject.Instantiate(_playerPrefab, _level.PlayerSpawnPoint.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        _player.SetInput(_gameScreen.Joystick);
        _cameraController.SetFollowTarget(_player.transform);

        var timeleft = 0;
        if (loadData)
        {
            timeleft = _playerData.Timeleft;

            if (timeleft <= 0)
            {
                timeleft = _levelTimeLeft;
            }
        }
        else
        {
            timeleft = _levelTimeLeft;
        }

        cancellationTokenSource = new CancellationTokenSource();
#pragma warning disable 4014
        _timeCounter.BeginCount(timeleft, cancellationTokenSource);
#pragma warning restore 4014
    }
}

public class TimeCounter
{
    public event Action<int> OnTimeChange;
    private bool isPause = false;
    public async UniTask BeginCount(int second, CancellationTokenSource cancellationToken = default)
    {
        OnTimeChange?.Invoke(second);
        int currentSec = second;
        while (currentSec > 0)
        {
            if (!isPause)
            {
                await UniTask.Delay(1000, cancellationToken: cancellationToken.Token);
                currentSec--;
                OnTimeChange?.Invoke(currentSec);
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }
}
