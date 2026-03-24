using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyBindingsSO _keys;

    [Header("Movement")]
    [SerializeField] private float _movementSpeedHor = 10;
    [SerializeField] private float _movementSpeedVer = 10;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovementHor();
        MovementVer();
    }

    private void MovementHor()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(_keys.forward))
        {
            direction = new Vector3(transform.forward.x, 0, transform.forward.z);
        }
        if (Input.GetKey(_keys.left))
        {
            direction = new Vector3(-transform.right.x, 0, -transform.right.z);
        }
        if (Input.GetKey(_keys.backward))
        {
            direction = new Vector3(-transform.forward.x, 0, -transform.forward.z);
        }
        if (Input.GetKey(_keys.right))
        {
            direction = new Vector3(transform.right.x, 0, transform.right.z);
        }

        _rb.AddForce(direction * _movementSpeedHor, ForceMode.Force);
    }

    private void MovementVer()
    {
        if (Input.GetKey(_keys.up))
        {
            _rb.AddForce(Vector3.up * _movementSpeedVer, ForceMode.Force);
        }
        if (Input.GetKey(_keys.down))
        {
            _rb.AddForce(Vector3.down * _movementSpeedVer, ForceMode.Force);
        }
    }
}