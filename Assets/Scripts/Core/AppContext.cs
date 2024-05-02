using System;
using System.Collections.Generic;
using Data;
using Game;
using Game.Enemy;
using Game.Level;
using UnityEngine;

namespace Core
{
    public class AppContext : MonoBehaviour, IAppContext
    { 
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private LevelController _gameLevelPrefab;
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private EnemyController _enemyPrefab;
        
        private Dictionary<Type, object> _registeredTypes;

        public void Construct()
        {
            _registeredTypes = new Dictionary<Type, object>();

            var playerData = new LevelData();

            RegisterInstance<ILevelData>(playerData);    
            RegisterInstance<IUImanager>(_uiManager);
            RegisterInstance<ICameraController>(_cameraController);
            RegisterInstance<LevelController>(_gameLevelPrefab);
            RegisterInstance<PlayerController>(_playerPrefab);
            RegisterInstance<EnemyController>(_enemyPrefab);
        }

        private void RegisterInstance<T>(T instance)
        {
            _registeredTypes.Add(typeof(T), instance);
        }

        public T Resolve<T>()
        {
            return (T)_registeredTypes[typeof(T)];
        }
    }
}
