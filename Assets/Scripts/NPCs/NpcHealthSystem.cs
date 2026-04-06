using System;
using UnityEngine;

public class NpcHealthSystem : MonoBehaviour
{
    public event Action<bool> OnNpcDie;

    [SerializeField] private NpcDataSO _data;
    [SerializeField] private int _damageNormal;
    [SerializeField] private int _damageBullet;
    //[SerializeField] private PlayerShoot _player;

    private NpcController _controller;
    private int _life;

    private void Awake()
    {
        _controller = GetComponent<NpcController>();
    }

    private void Start()
    {
        _life = _data.life;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void TakeDamage(int damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            _life = 0;
            OnNpcDie?.Invoke(_controller._isEnemy);
        }
    }

    private void OnNormalShot_TakeDamage()
    {
        TakeDamage(_damageNormal);
    }

    private void OnBulletShot_TakeDamage()
    {
        TakeDamage(_damageBullet);
    }
}