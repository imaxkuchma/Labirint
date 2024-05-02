using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LevelData : ILevelData
    {
        public int attemptCount;
        public int timeLeft;
        public SerializedVector3 playerPosition;
        public SerializedVector3[] emeniesPosition;
        public int AttemptCount => attemptCount;
        public int TimeLeft => timeLeft;

        public SerializedVector3 PlayerPosition => playerPosition;
        public SerializedVector3[] EmeniesPosition => emeniesPosition;

        public void Load()
        {
            if (PlayerPrefs.HasKey("LevelData"))
            {
                var json = PlayerPrefs.GetString("LevelData");
                var levelData = JsonConvert.DeserializeObject<LevelData>(json);
                attemptCount = levelData.attemptCount;
                timeLeft = levelData.timeLeft;
                playerPosition = levelData.playerPosition;
                emeniesPosition = levelData.emeniesPosition;
            }
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this);
            Debug.Log(json);
            PlayerPrefs.SetString("LevelData", json);
        }

        public void SetTimeLeft(int timeLeft)
        {
            this.timeLeft = timeLeft;
        }

        public void SetAttemptCount(int attemptCount)
        {
            this.attemptCount = attemptCount;
        }
        
        public void SetPlayerPosition(Vector3 position)
        {
            this.playerPosition = new SerializedVector3(position.x,position.y,position.z);
        }
        
        public void SetEmeniesPosition(Vector3[] positions)
        {
            if (positions == null)
            {
                this.emeniesPosition = new SerializedVector3[1] {new SerializedVector3(0,0,0)};
            }
            
            this.emeniesPosition = new SerializedVector3[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                this.emeniesPosition[i] = new SerializedVector3(positions[i].x,positions[i].y,positions[i].z);
            }
        }
    }
}