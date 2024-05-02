using UnityEngine;

namespace Game.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemiesSpawnPoints;
        [SerializeField] private Transform[] _waypoints;
        public Transform PlayerSpawnPoint => _playerSpawnPoint;
        public Transform[] EnemiesSpawnPoints => _enemiesSpawnPoints;
        public Vector3[] GetTwoWaypointsByEnemyIndex(int enemyIndex)
        {
            if (enemyIndex * 2 + 1 < _waypoints.Length)
            {
                return new Vector3[2] {_waypoints[enemyIndex * 2].position, _waypoints[ enemyIndex*2 +1].position};
            }
            
            return new Vector3[1] {transform.position};
        }
    }
}
