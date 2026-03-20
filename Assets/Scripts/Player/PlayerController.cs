using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _movementSpeedHor = 10;
    [SerializeField] private float _movementSpeedVer = 10;

    [Header("Rotation")]
    [SerializeField] private float _rotationSpeedHor = 10;
    [SerializeField] private float _rotationSpeedVer = 10;
    [SerializeField] private float _rotationMinVer = 10;
    [SerializeField] private float _rotationMaxVer = 10;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovementHor();
        MovementVer();
        Rotation();
    }

    private void MovementHor()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction = new Vector3(transform.forward.x, 0, transform.forward.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction = new Vector3(-transform.right.x, 0, -transform.right.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction = new Vector3(-transform.forward.x, 0, -transform.forward.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = new Vector3(transform.right.x, 0, transform.right.z);
        }

        _rb.AddForce(direction * _movementSpeedHor, ForceMode.Force);
    }

    private void MovementVer()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _movementSpeedVer, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _rb.AddForce(Vector3.down * _movementSpeedVer, ForceMode.Force);
        }
    }

    private void Rotation()
    {
        Vector3 rotation = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);

        rotation.x *= _rotationSpeedVer;
        rotation.y *= _rotationSpeedHor;
        rotation *= Time.deltaTime;

        if (-rotation.x <= _rotationMinVer)
        {
            rotation.x = -_rotationMinVer;
        }

        if (-rotation.x >= _rotationMaxVer)
        {
            rotation.x = -_rotationMaxVer;
        }

        transform.localEulerAngles += rotation;
    }
}