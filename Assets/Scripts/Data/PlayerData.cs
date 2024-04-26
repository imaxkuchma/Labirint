using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerData
{    
    int AttemptCount { get;}
    int Timeleft { get; }
    void Load();
    void Save();
    void SetTimeleft(int timeLeft);
    void SetAttemptCount(int count);
}

public class PlayerData : IPlayerData
{
    public int attemptCount;
    public int timeleft;

    public int AttemptCount => attemptCount;
    public int Timeleft => timeleft;

    public void Load()
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            var json = PlayerPrefs.GetString("PlayerData");
            var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            attemptCount = playerData.attemptCount;
            timeleft = playerData.timeleft;
        }
    }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(this);
        PlayerPrefs.SetString("PlayerData", json);
    }

    public void SetTimeleft(int timeLeft)
    {
        timeleft = timeLeft;
    }

    public void SetAttemptCount(int count)
    {
        attemptCount = count;
    }
}