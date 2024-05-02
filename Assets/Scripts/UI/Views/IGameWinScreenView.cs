using System;

namespace UI.Views
{
    public interface IGameWinScreenView : IView
    {
        event Action OnReplayButtonClick;
    }
}

