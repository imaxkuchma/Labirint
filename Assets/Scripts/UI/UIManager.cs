using System.Collections;
using System.Collections.Generic;
using UI;
using UI.Views;
using UnityEngine;

public interface IUImanager
{
    T GetScreen<T>(ScreenType type);
}

public class UIManager : MonoBehaviour, IUImanager
{
    [SerializeField] private List<BaseScreenView> _screens;

    public T GetScreen<T>(ScreenType type)
    {
        var screen = _screens.Find(x => x.Type == type);
        return screen.GetComponent<T>();
    } 
}
