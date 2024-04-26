
public abstract class BaseState<T>: IState
{
    protected T _stateMachine { get; private set; }

    public BaseState(T stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void Update();
}
