using System;
using UnityEngine;

namespace Game
{
    public interface IGameController
    {
        event Action OnPlayerWin;
        event Action OnPlayerLost;
        int TimeLeft { get; }
        int AttemptCount { get; }
        Vector3[] GetEnemiesPositions();
        Vector3 GetPlayerPosition();
        void PlayGame(bool loadData = false, bool isResume = false);
    }
}