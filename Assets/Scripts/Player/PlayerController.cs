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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        readableMaxSpeed = _maxSpeed;
    }

    private void FixedUpdate()
    {
        MovementHor();
        MovementVer();
        CheckSpeed();
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
}