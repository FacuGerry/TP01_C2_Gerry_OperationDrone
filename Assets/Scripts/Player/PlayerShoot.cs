using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private KeyBindingsSO _keys;
    [SerializeField] private Transform _shootingPos;

    [Header("Second bullet stats")]
    [SerializeField] private List<GameObject> _bullets = new List<GameObject>();
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletHeight;
    [SerializeField] private float _bulletDistance;
    [SerializeField] private GameObject _cheatLine;

    private List<Rigidbody> _bulletRb = new List<Rigidbody>();
    private bool _isShooting = false;
    private bool _startedShooting = false;

    private IEnumerator _corroutineShoot;

    private void Awake()
    {
        foreach (GameObject bullet in _bullets)
            _bulletRb.Add(bullet.GetComponent<Rigidbody>());
    }

    private void Update()
    {
        if (Input.GetKey(_keys.shoot))
            _isShooting = true;

        if (Input.GetKeyDown(_keys.secondShoot))
            SecondShoot();

        if (Input.GetKeyDown(_keys.showCheat))
            ShowCheat();

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
            if (_corroutineShoot != null)
                StopCoroutine(_corroutineShoot);
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

            if (ray.collider != null && ray.collider.TryGetComponent(out NpcHealthSystem enemy))
            {

                Debug.Log("Hit a NPC");
            }
            else
                Debug.Log("Skill issue");

            yield return null;
        }
    }

    private void SecondShoot()
    {
        bool isSearching = true;
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].activeInHierarchy && isSearching)
            {
                _bullets[i].transform.position = _shootingPos.position;
                _bullets[i].SetActive(true);

                Vector3 targetPos = transform.forward * _bulletDistance;
                targetPos.y += _bulletHeight;
                Vector3 bulletDirection = (targetPos - _bullets[i].transform.position).normalized;

                _bulletRb[i].linearVelocity = bulletDirection * _bulletSpeed;
                Debug.Log("Player threw a bullet to (" + bulletDirection.x + ", " + bulletDirection.y + ")");

                isSearching = false;
            }
        }
    }

    private void ShowCheat()
    {
        _cheatLine.SetActive(!_cheatLine.activeInHierarchy);
    }
}
