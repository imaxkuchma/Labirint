using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameScreenView : BaseScreenView, IGameScreenView
{
    [SerializeField] private TextMeshProUGUI _attemptText;
    [SerializeField] private TextMeshProUGUI _timeLeftText;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private JoystickController _joystick;

    public event Action OnPauseButtonClick;

    public override ScreenType Type => ScreenType.GameScreen;
    public IInputSystem Joystick => _joystick;

    protected override void OnAwake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            OnPauseButtonClick?.Invoke();
        });
    }

    public void SetNumberAttempts(int levelIndex)
    {
        _attemptText.text = $"Attempt: {levelIndex}";
    }

    public void SetTimeleft(int value)
    {
        TimeSpan result = TimeSpan.FromSeconds(value);
        string fromTimeString = result.ToString("mm':'ss");

        _timeLeftText.text = fromTimeString;
    }
}
