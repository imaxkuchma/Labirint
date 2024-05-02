using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public interface ILevelData
    {    
        int AttemptCount { get;}
        int TimeLeft { get; }
        SerializedVector3 PlayerPosition { get; }
        SerializedVector3[] EmeniesPosition { get; }
        void Load();
        void Save();
        void SetTimeLeft(int timeLeft);
        void SetAttemptCount(int count);
        void SetPlayerPosition(Vector3 position);
        void SetEmeniesPosition(Vector3[] positions);
    }
}