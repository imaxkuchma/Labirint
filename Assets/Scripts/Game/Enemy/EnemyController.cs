using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{  
    [SerializeField] private float _speedMove = 3.5f;
    [SerializeField] private Transform[] _waypoints;

    [Header("Target detector")]
    [Range(0,360)]
    [SerializeField] private float _viewAngle = 60.0f;
    [SerializeField] private float _targetRadiusDetect;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    private NavMeshAgent _agent;
    private Vector3 _movePosition;
    private Coroutine _setMovePositionCoroutine;
    private float _timeWaitInIdle;

    private IStateMachine<EnemyController> _stateMachine;

    public Transform Target { get; private set; }
    public bool CanMove { get; private set; } = true;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _stateMachine = new StateMachine<EnemyController>(this);
        _stateMachine.AddState<EnemyIdleState>(new EnemyIdleState(_stateMachine));
        _stateMachine.AddState<EnemyMoveState>(new EnemyMoveState(_stateMachine));   
    }

    private void OnEnable()
    {
        StartCoroutine(DetectTargetDelay());

        _stateMachine.SwitchState<EnemyIdleState>();
    }

    private Vector3 GetRandomWaypoint()
    {
        return _waypoints[Random.Range(0, _waypoints.Length)].position;
    }

    private void Update()
    {
        if(_stateMachine.CurrentState != null)
        {
            _stateMachine.CurrentState.Update();
        }
    }

    public void StartMove()
    {
        _agent.isStopped = false;
        _agent.speed = _speedMove;
        _movePosition = GetRandomWaypoint();
        _setMovePositionCoroutine = StartCoroutine(SetMovePosition());
    }

    public float DistanceToMoveTarget()
    {
        return Vector3.Distance(transform.position, _movePosition);
    }

    private IEnumerator SetMovePosition()
    {
        while (true)
        {
            if (Target != null)
            {
                _movePosition = Target.position;
            }
            _agent.SetDestination(_movePosition);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void StopMove()
    {
        _agent.isStopped = true;
        if(_setMovePositionCoroutine != null)
        {
            StopCoroutine(_setMovePositionCoroutine);
        }
    }

    private IEnumerator DetectTargetDelay()
    {
        while (true)
        {
            Target = DetectTarget();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public Transform DetectTarget()
    {
        Transform target = null;
        var targets = Physics.OverlapSphere(transform.position, _targetRadiusDetect, _targetMask);
        for(var i = 0; i< targets.Length; i++)
        {
            var targetTransform = targets[i].transform;
            var dirToTarget = (targetTransform.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) <= _viewAngle / 2)
            {
                var disToTarger = Vector3.Distance(transform.position, targetTransform.position);
                if(!Physics.Raycast(transform.position, dirToTarget, disToTarger, _obstacleMask))
                {
                    
                    target = targetTransform;
                    break;
                }
            }
        }
        return target;
    }


    public void StartWaitIdle()
    {
        _timeWaitInIdle = Random.Range(0, 1.5f);

        StartCoroutine(WaitDelay(_timeWaitInIdle));
    }

    private IEnumerator WaitDelay(float delay)
    {
        _stateMachine.Controller.CanMove = false; 
        yield return new WaitForSeconds(delay);
        _stateMachine.Controller.CanMove = true;
    }
}