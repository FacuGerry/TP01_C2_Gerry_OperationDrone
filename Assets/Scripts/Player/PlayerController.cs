using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10;

    [SerializeField] private float _rotationSpeed = 10;

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
            direction = transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction = -transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction = -transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = transform.right;
        }

        _rb.AddForce(direction * _movementSpeed, ForceMode.Force);
    }

    private void MovementVer()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _movementSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _rb.AddForce(Vector3.down * _movementSpeed, ForceMode.Force);
        }
    }

    private void Rotation()
    {
        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        rotation *= _rotationSpeed;
        transform.Rotate(rotation);
    }
}