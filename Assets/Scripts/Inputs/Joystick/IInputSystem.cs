using System;
using UnityEngine;

public interface IInputSystem
{
    public event Action<bool> OnStickMove;
    public event Action<Vector2> OnStickDirection;
}
