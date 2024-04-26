using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events 
{
    public static event Action OnOnPlayerDamaged;

    public static void RaiseOnPlayerDamaged()
    {
        OnOnPlayerDamaged?.Invoke();
    }
}
