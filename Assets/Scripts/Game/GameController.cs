using System;
using Core;
using Data;
using Game.Enemy;
using Game.Level;
using UI;
using UI.Views;
using UnityEngine;

namespace Game
{
    public class GameController : IGameController
    {
        private const int _levelTimeLeft = 30;
        
        private readonly ILevelData _levelData;
        private readonly ICameraController _cameraController;
        private readonly LevelController _levelPrefab;
        private readonly PlayerController _playerPrefab;
        private readonly EnemyController _enemyPrefab;
        private readonly IGameScreenView _gameScreen;
        private readonly TimeCounter _timeCounter;
        
        private LevelController _level;
        private PlayerController _player;
        private EnemyController[] _enemies;


        public int TimeLeft { get; private set; }
        public int AttemptCount { get; private set; } = 0;
        public Vector3[] GetEnemiesPositions()
        {
            if (_enemies == null) return null;
            
            var enemiesPosition = new Vector3[_enemies.Length];
            for (int i = 0; i < _enemies.Length; i++)
            {
                enemiesPosition[i] = _enemies[i].transform.position;
            }
            return enemiesPosition;
        }
        
        public Vector3 GetPlayerPosition()
        {
            if (_player == null) return Vector3.zero;
            
            return _player.transform.position;
        }
        public event Action OnPlayerWin;
        public event Action OnPlayerLost;
    
        public GameController(IAppContext appContext)
        {
            _levelData = appContext.Resolve<ILevelData>();
            _levelPrefab = appContext.Resolve<LevelController>();
            _playerPrefab = appContext.Resolve<PlayerController>();
            _enemyPrefab = appContext.Resolve<EnemyController>();
                
            _cameraController = appContext.Resolve<ICameraController>();

            var uiManager = appContext.Resolve<IUImanager>();
            _gameScreen = uiManager.GetScreen<IGameScreenView>(ScreenType.GameScreen);

            _timeCounter = new TimeCounter();
            _timeCounter.OnTimeChange += OnTimeChange;
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

        public void PlayGame(bool loadGame = false, bool isResume = false)
        { 
            if(isResume) return;
            
            if (_level != null)
            {
                GameObject.Destroy(_level.gameObject);
            }

            if (_player != null)
            {
                GameObject.Destroy(_player.gameObject);
            }

            if (_enemies != null)
            {
                foreach (EnemyController enemy in _enemies)
                {
                    GameObject.Destroy(enemy.gameObject);
                }
            }
            
            _level = GameObject.Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            
            //Create player
            Vector3 playerSpawnPosition = _level.PlayerSpawnPoint.position + new Vector3(0, 0.5f, 0);
            if (loadGame)
            {
                playerSpawnPosition = new Vector3(_levelData.PlayerPosition.x,_levelData.PlayerPosition.y,_levelData.PlayerPosition.z);
            }
            _player = GameObject.Instantiate(_playerPrefab, playerSpawnPosition , Quaternion.identity);
            _player.Init(this, _gameScreen.Joystick);
            _cameraController.SetFollowTarget(_player.transform);
            
            //Create enemies
            _enemies = new EnemyController[_level.EnemiesSpawnPoints.Length];
            for (int i = 0; i < _level.EnemiesSpawnPoints.Length; i++)
            {
                Vector3 enemySpawnPosition = _level.EnemiesSpawnPoints[i].position + new Vector3(0, 0.5f, 0);
                if (loadGame)
                {
                    enemySpawnPosition = new Vector3(_levelData.EmeniesPosition[i].x,_levelData.EmeniesPosition[i].y,_levelData.EmeniesPosition[i].z);
                }
                EnemyController enemy = GameObject.Instantiate(_enemyPrefab,  enemySpawnPosition, Quaternion.identity);
                
                enemy.Init(_level.GetTwoWaypointsByEnemyIndex(i));
                _enemies[i] = enemy;
            }
            
            //Set level info
            var timeLeft = 0;
            if (loadGame)
            {
                AttemptCount = _levelData.AttemptCount + 1;
                timeLeft = _levelData.TimeLeft;

                if (timeLeft <= 0)
                {
                    timeLeft = _levelTimeLeft;
                }
            }
            else
            {
                AttemptCount++;
                timeLeft = _levelTimeLeft;
            }
            
            _gameScreen.SetNumberAttempts(AttemptCount);
            _gameScreen.SetTimeleft(timeLeft);
        
#pragma warning disable 4014
            _timeCounter.BeginCount(timeLeft);
#pragma warning restore 4014 
        }

        public void PlayerCaught()
        {
            OnPlayerLost?.Invoke();
        }
        
        public void PlayerHasFinished()
        {
            OnPlayerWin?.Invoke();
            AttemptCount = 0;
        }
    }
}