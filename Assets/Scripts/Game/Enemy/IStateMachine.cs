namespace Game.Enemy
{
    public interface IStateMachine<T>
    {
        T Controller { get; }
        public IState CurrentState { get; }
        void SwitchState<IState>();
        void AddState<TState>(TState state) where TState : IState;
    }
}