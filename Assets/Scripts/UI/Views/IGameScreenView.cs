
using System;

public interface IGameScreenView : IView
{
    void SetNumberAttempts(int levelIndex);
    void SetTimeleft(int value);   
    IInputSystem Joystick { get; }

    event Action OnPauseButtonClick;
}
