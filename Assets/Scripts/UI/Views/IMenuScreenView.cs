
using System;

public interface IMenuScreenView : IView
{
    event Action OnLoadButtonClick;
    event Action OnSaveButtonClick;
}
