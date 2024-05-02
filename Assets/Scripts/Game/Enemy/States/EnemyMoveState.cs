
using Game.Enemy;

public class EnemyMoveState: BaseState<IStateMachine<EnemyController>>
{
    public EnemyMoveState(IStateMachine<EnemyController> stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        _stateMachine.Controller.StartMove();
    }

    public override void Update()
    {
        if (_stateMachine.Controller.DistanceToMoveTarget() <= 1.0f && _stateMachine.Controller.Target == null)
        {
            _stateMachine.SwitchState<EnemyIdleState>();          
        }
    }

    public override void Exit()
    {
        _stateMachine.Controller.StopMove();
    }
}
