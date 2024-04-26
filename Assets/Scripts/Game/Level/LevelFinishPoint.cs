using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishPoint : MonoBehaviour
{
    public event Action OnPlayerFinished; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            OnPlayerFinished?.Invoke();
        }
    }
}
