using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private KeyBindingsSO _keys;
    [SerializeField] private Transform _shootingPos;
    [SerializeField] private float _normalBulletDistance;

    [Header("Second bullet stats")]
    [SerializeField] private List<GameObject> _bullets = new List<GameObject>();
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletHeight;
    [SerializeField] private float _bulletDistance;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private GameObject _cheatLine;

    private List<Rigidbody> _bulletRb = new List<Rigidbody>();
    private bool _isShooting = false;
    private bool _startedShooting = false;

    private bool _isPaused = false;
    private IEnumerator _corroutineShoot;

    private void Awake()
    {
        foreach (GameObject bullet in _bullets)
            _bulletRb.Add(bullet.GetComponent<Rigidbody>());
    }

    private void OnEnable()
    {
        PauseGame.OnPause += OnPause_PauseGame;
    }

    private void Update()
    {
        if (!_isPaused)
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
    }

    private void OnDisable()
    {
        PauseGame.OnPause -= OnPause_PauseGame;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shooting()
    {
        while (_isShooting)
        {
            RaycastHit ray;
            if (Physics.Raycast(_shootingPos.transform.position, transform.forward, out ray, _normalBulletDistance))
                if (ray.collider != null && ray.collider.TryGetComponent(out NpcHealthSystem npc))
                {
                    npc.OnNormalShot_TakeDamage(_bulletDamage);
                    Debug.Log("Hit an NPC");
                }
            yield return new WaitForSeconds(0.3f);
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

                Vector3 targetPos = transform.position + transform.forward * _bulletDistance;
                targetPos.y += _bulletHeight;
                Vector3 bulletDirection = (targetPos - _bullets[i].transform.position).normalized;

                _bulletRb[i].linearVelocity = bulletDirection * _bulletSpeed;
                Debug.Log("Player threw a bullet to (" + bulletDirection.x + ", " + bulletDirection.y + ", " + bulletDirection.z + ")");

                isSearching = false;
            }
        }
    }

    private void ShowCheat()
    {
        _cheatLine.SetActive(!_cheatLine.activeInHierarchy);
    }

    private void OnPause_PauseGame(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
