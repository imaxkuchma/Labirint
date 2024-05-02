namespace Core.FMS
{
    public interface IGameStateMachine
    {
        void AddState<T>(T state) where T : IGameState;
        void SwitchState<T>();
    }
}
