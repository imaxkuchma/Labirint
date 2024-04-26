using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppContext : MonoBehaviour, IAppContext
{ 
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Level _gameLevelPrefab;
    [SerializeField] private PlayerController _playerPrefab;

    private Dictionary<Type, object> _registeredTypes;

    public void Construct()
    {
        _registeredTypes = new Dictionary<Type, object>();

        var playerData = new PlayerData();
        //playerData.Load();

        RegisterInstance<IPlayerData>(playerData);    
        RegisterInstance<IUImanager>(_uiManager);
        RegisterInstance<ICameraController>(_cameraController);
        RegisterInstance<Level>(_gameLevelPrefab);
        RegisterInstance<PlayerController>(_playerPrefab);
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
