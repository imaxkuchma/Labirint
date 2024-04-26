using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private LevelFinishPoint _playerFinishPoint;

    public Transform PlayerSpawnPoint => _playerSpawnPoint;
    public LevelFinishPoint PlayerFinishPoint => _playerFinishPoint;
}
