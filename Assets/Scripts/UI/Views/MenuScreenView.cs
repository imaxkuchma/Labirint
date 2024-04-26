using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenView : BaseScreenView, IMenuScreenView
{
    [SerializeField] private Button _loadLevelButton;
    [SerializeField] private Button _saveLevelButton;

    public override ScreenType Type => ScreenType.MenuScreen;

    public event Action OnLoadButtonClick;
    public event Action OnSaveButtonClick;

    public void Awake()
    {
        _loadLevelButton.onClick.AddListener(() =>
        {
            OnLoadButtonClick?.Invoke();
        });

        _saveLevelButton.onClick.AddListener(() =>
        {
            OnSaveButtonClick?.Invoke();
        });
    }
}
