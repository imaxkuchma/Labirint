using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;

public class EnemyIdleState : BaseState<IStateMachine<EnemyController>>
{
    private float _waitInIdleTimeMin = 0.2f;
    private float _waitInIdleTimeMax = 1.5f;
    private float _waitInIdleTime;
    private float _stateEnterTime;

    public EnemyIdleState(IStateMachine<EnemyController> stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {     
        _waitInIdleTime = Random.Range(_waitInIdleTimeMin, _waitInIdleTimeMax);
        _stateEnterTime = Time.time;
    }

    public override void Update()
    {
        if(_stateMachine.Controller.Target != null || (Time.time - _stateEnterTime) >= _waitInIdleTime)
        {
            _stateMachine.SwitchState<EnemyMoveState>();
        }
    }

    public override void Exit()
    {
        
    }
}
