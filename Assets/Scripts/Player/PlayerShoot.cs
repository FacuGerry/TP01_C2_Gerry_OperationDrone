using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private KeyBindingsSO _keys;
    [SerializeField] private Transform _shootingPos;

    [Header("Second bullet stats")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDistance;
    [SerializeField] private Rigidbody _bulletRb;

    private bool _isShooting = false;
    private bool _startedShooting = false;

    private IEnumerator _corroutineShoot;

    private void Update()
    {
        if (Input.GetKey(_keys.shoot))
            _isShooting = true;

        if (Input.GetKey(_keys.secondShoot))
            SecondShoot();

        if (Input.GetKeyUp(_keys.shoot))
        {
            _isShooting = false;
            _startedShooting = false;
        }

        if (_isShooting && !_startedShooting)
        {
            _startedShooting = true;
            if (_corroutineShoot != null)
                StopCoroutine(_corroutineShoot);

            _corroutineShoot = Shooting();
            StartCoroutine(_corroutineShoot);
        }

        if (!_isShooting)
        {
            if (_corroutineShoot != null)
                StopCoroutine(_corroutineShoot);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shooting()
    {
        while (_isShooting)
        {
            RaycastHit2D ray = Physics2D.Raycast(_shootingPos.position, transform.forward);

            if (ray.collider != null && ray.collider.CompareTag("NPC"))
                Debug.Log("Hit a NPC");
            else
                Debug.Log("Skill issue");

            yield return null;
        }
    }

    private void SecondShoot()
    {
        Vector3 targetPos = transform.forward * _bulletDistance;
        Vector3 bulletDirection = (targetPos - gameObject.transform.position).normalized;
        _bulletRb.linearVelocity = bulletDirection * _bulletSpeed;
        Debug.Log("Player threw a bullet to (" + bulletDirection.x + ", " + bulletDirection.y + ")");
    }
}
