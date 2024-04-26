using System;

public interface IGameLostScreenView : IView
{
    event Action OnReplayGameButtonClick;
}

