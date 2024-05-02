using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;

public class StateMachine<T> : IStateMachine<T>
{
    private IState _currentState;
    private readonly Dictionary<Type, IState> _states;

    public IState CurrentState => _currentState;

    public T Controller {get; private set;}

    public StateMachine(T controller)
    {
        _states = new Dictionary<Type, IState>();
        Controller = controller;
    }

    public void AddState<S>(S state) where S : IState
    {
        if (!_states.ContainsKey(state.GetType()))
        {
            _states[state.GetType()] = state;
        }
    }


    public void SwitchState<IState>()
    {
        if (!_states.ContainsKey(typeof(IState)))
        {
            return;
        }

        if(_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = _states[typeof(IState)];

        _currentState.Enter();
    }
}
