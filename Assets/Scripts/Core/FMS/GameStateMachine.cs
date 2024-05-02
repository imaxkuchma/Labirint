using System;
using System.Collections.Generic;

namespace Core.FMS
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IGameState> _states;

        private IGameState _currentState;
        public GameStateMachine()
        {
            _states = new Dictionary<Type, IGameState>();
        }

        public void AddState<T>(T state) where T : IGameState
        {
            if (!_states.ContainsKey(typeof(T)))
            {
                _states[typeof(T)] = state;
            }
        }

        public void SwitchState<T>()
        {
            if (!_states.ContainsKey(typeof(T)))
            {
                return;
            }
            var state = _states[typeof(T)];
            if (_currentState !=  null)
            {
                _currentState.Exit();
            }

            _currentState = state;
            _currentState.Enter();
        }
    }
}
