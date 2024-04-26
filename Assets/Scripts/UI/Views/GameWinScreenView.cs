using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWinScreenView : BaseScreenView, IGameWinScreenView
{
    [SerializeField] private Button _replayButtonButton;
    public override ScreenType Type => ScreenType.GameWinScreen;

    public event Action OnReplayButtonClick;

    protected override void OnAwake()
    {
        _replayButtonButton.onClick.AddListener(()=>
        {
            OnReplayButtonClick?.Invoke();
        });
    }
}

