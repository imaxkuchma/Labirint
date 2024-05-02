using System;

namespace UI.Views
{
    public interface IGameLostScreenView : IView
    {
        event Action OnReplayGameButtonClick;
    }
}

