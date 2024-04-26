using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : BaseState<IStateMachine<EnemyController>>
{
    public EnemyIdleState(IStateMachine<EnemyController> stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        _stateMachine.Controller.StartWaitIdle();
    }

    public override void Update()
    {
        if(_stateMachine.Controller.Target != null || _stateMachine.Controller.CanMove)
        {
            _stateMachine.SwitchState<EnemyMoveState>();
        }
    }

    public override void Exit()
    {
        
    }
}
