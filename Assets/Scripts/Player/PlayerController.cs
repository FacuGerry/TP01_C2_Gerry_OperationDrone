using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyBindingsSO _keys;

    [Header("Movement")]
    [SerializeField] private float _movementSpeedHor = 10;
    [SerializeField] private float _movementSpeedVer = 10;
    [SerializeField] private float _maxSpeed;

    public float readableMaxSpeed { get; private set; }

    public Rigidbody _rb { get; private set; }

    private bool _isAlive;
    private bool _isPaused = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        readableMaxSpeed = _maxSpeed;
        _isAlive = true;
    }

    private void OnEnable()
    {
        HealthSystem.OnPlayerDie += OnPlayerDie_StopMovement;
        PauseGame.OnPause += OnPause_PauseGame;
    }

    private void OnDisable()
    {
        HealthSystem.OnPlayerDie -= OnPlayerDie_StopMovement;
        PauseGame.OnPause -= OnPause_PauseGame;
    }

    private void FixedUpdate()
    {
        if (_isAlive && !_isPaused)
        {
            MovementHor();
            MovementVer();
            CheckSpeed();
        }
    }

    private void MovementHor()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(_keys.forward))
            direction = new Vector3(transform.forward.x, 0, transform.forward.z);

        if (Input.GetKey(_keys.left))
            direction = new Vector3(-transform.right.x, 0, -transform.right.z);

        if (Input.GetKey(_keys.backward))
            direction = new Vector3(-transform.forward.x, 0, -transform.forward.z);

        if (Input.GetKey(_keys.right))
            direction = new Vector3(transform.right.x, 0, transform.right.z);

        _rb.AddForce(direction * _movementSpeedHor, ForceMode.Force);
    }

    private void MovementVer()
    {
        if (Input.GetKey(_keys.up))
            _rb.AddForce(Vector3.up * _movementSpeedVer, ForceMode.Force);

        if (Input.GetKey(_keys.down))
            _rb.AddForce(Vector3.down * _movementSpeedVer, ForceMode.Force);
    }

    private void CheckSpeed()
    {
        if (_rb.linearVelocity.x >= readableMaxSpeed)
            _rb.linearVelocity = new Vector3(readableMaxSpeed, _rb.linearVelocity.y, _rb.linearVelocity.z);

        if (_rb.linearVelocity.y >= readableMaxSpeed)
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, readableMaxSpeed, _rb.linearVelocity.z);

        if (_rb.linearVelocity.z >= readableMaxSpeed)
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, readableMaxSpeed);

        if (_rb.linearVelocity.x <= -readableMaxSpeed)
            _rb.linearVelocity = new Vector3(-readableMaxSpeed, _rb.linearVelocity.y, _rb.linearVelocity.z);

        if (_rb.linearVelocity.y <= -readableMaxSpeed)
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, -readableMaxSpeed, _rb.linearVelocity.z);

        if (_rb.linearVelocity.z <= -readableMaxSpeed)
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, -readableMaxSpeed);
    }

    private void OnPlayerDie_StopMovement()
    {
        _isAlive = false;
    }

    private void OnPause_PauseGame(bool isPaused)
    {
        _isPaused = isPaused;
    }
}