using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    [SerializeField] private float _chancesForEnemy;
    [SerializeField] private float _shootDistance;
    [SerializeField] private GameObject _player;

    private List<EnemyStates> _states = new List<EnemyStates>();
    private EnemyStates _currentState;
    private Animator _anim;
    private Rigidbody _rb;
    private NavMeshAgent _agent;

    private bool _isEnemy = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();

        _states.Add(new StateIdle());
        _states.Add(new StateWalk());
        _states.Add(new StateShoot());

        foreach (EnemyStates state in _states)
            state.Initialize(_anim, _rb, this, _agent);

        _currentState = FindState(StateType.Idle);
        _currentState.OnEnter();
    }

    private void Start()
    {
        float rand = Random.value;
        if (rand >= _chancesForEnemy)
            _isEnemy = true;

        SwitchState(FindState(StateType.Walk));
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState.OnUpdate();

        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) < _shootDistance)
            SwitchState(FindState(StateType.Shoot));
        else
            SwitchState(FindState(StateType.Walk));
    }

    private void SwitchState(EnemyStates newState)
    {
        if (_currentState == newState)
            return;

        _currentState.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    private EnemyStates FindState(StateType stateToFind)
    {
        foreach (EnemyStates state in _states)
            if (state.state == stateToFind)
                return state;

        return null;
    }
}
