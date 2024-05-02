using Core.FMS;
using Game;
using UnityEngine;

namespace Core
{
    public class App : MonoBehaviour
    {
        [SerializeField] private AppContext _appContext;

        private IGameStateMachine _gameStateMachine;

        private void Awake()
        {
            _appContext.Construct();

            var gameController = new GameController(_appContext);

            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.AddState<PlayGameState>(new PlayGameState(_gameStateMachine, _appContext, gameController));
            _gameStateMachine.AddState<PauseGameState>(new PauseGameState(_gameStateMachine, _appContext, gameController));
            _gameStateMachine.AddState<LostGameState>(new LostGameState(_gameStateMachine, _appContext, gameController));
            _gameStateMachine.AddState<WinGameState>(new WinGameState(_gameStateMachine, _appContext, gameController));
        }

        public void Start()
        {
            _gameStateMachine.SwitchState<PlayGameState>();
        }
    }
}
