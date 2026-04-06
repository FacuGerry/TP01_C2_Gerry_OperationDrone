using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _anim;
    [SerializeField] private NpcDataSO _data;

    [Header("Gun")]
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _walkPos;
    [SerializeField] private Transform _shootPos;

    private List<EnemyStates> _states = new List<EnemyStates>();
    private EnemyStates currentState;
    private Rigidbody _rb;
    private NavMeshAgent _agent;

    public bool isEnemy { get; private set; }

    private bool _isShooting;

    private IEnumerator _corroutineShoot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();

        _states.Add(new StateIdle());
        _states.Add(new StateWalk());
        _states.Add(new StateShoot());

        foreach (EnemyStates state in _states)
            state.Initialize(_anim, _rb, this, _agent);

        currentState = FindState(StateType.Idle);
        currentState.OnEnter();
    }

    private void Start()
    {
        transform.position = new Vector3(positions[0].x, transform.position.y, positions[0].z);

        float rand = Random.value;
        if (rand >= _data.chanceToBeEnemy)
            isEnemy = true;
        else
            isEnemy = false;

        SwitchState(FindState(StateType.Walk));

        _agent.speed = _data.speed;
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();

        if (isEnemy)
            CheckForPlayer();

        MoveGun();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shooting()
    {
        while (_isShooting)
        {

            yield return null;
        }
        yield return null;
    }

    public void EnableShooting(bool isShooting)
    {
        if (isShooting)
        {
            if (_corroutineShoot != null) { }
            else
            {
                _isShooting = true;

                _corroutineShoot = Shooting();
                StartCoroutine(_corroutineShoot);
            }
        }
        else
        {
            _isShooting = false;

            if (_corroutineShoot != null)
                StopCoroutine(_corroutineShoot);
        }
    }

    private void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _data.distanceToShoot)
            SwitchState(FindState(StateType.Shoot));
        else
            SwitchState(FindState(StateType.Walk));
    }

    private void SwitchState(EnemyStates newState)
    {
        if (currentState == newState)
            return;

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    private EnemyStates FindState(StateType stateToFind)
    {
        foreach (EnemyStates state in _states)
            if (state.state == stateToFind)
                return state;

        return null;
    }

    private void MoveGun()
    {
        if (currentState == FindState(StateType.Walk))
        {
            _weapon.transform.position = _walkPos.position;
            _weapon.transform.localEulerAngles = _walkPos.localEulerAngles;
        }
        if (currentState == FindState(StateType.Shoot))
        {
            _weapon.transform.position = _shootPos.position;
            _weapon.transform.localEulerAngles = _shootPos.localEulerAngles;
        }
    }
}
