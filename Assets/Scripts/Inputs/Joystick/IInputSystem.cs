using System;
using UnityEngine;

namespace Inputs.Joystick
{
    public interface IInputSystem
    {
        public event Action<bool> OnStickMove;
        public event Action<Vector2> OnStickDirection;
    }
}
