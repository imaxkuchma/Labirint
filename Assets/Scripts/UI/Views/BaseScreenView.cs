using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreenView : MonoBehaviour, IView
{
    public abstract ScreenType Type { get; }

    private void Awake()
    {
        OnAwake();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    protected virtual void OnAwake()
    {

    }
}
