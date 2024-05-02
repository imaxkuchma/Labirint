
using System;

namespace UI.Views
{
    public interface IMenuScreenView : IView
    {
        event Action OnLoadButtonClick;
        event Action OnSaveButtonClick;
    }
}
