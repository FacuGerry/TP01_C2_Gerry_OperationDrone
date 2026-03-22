using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static event Action<int, int> OnUpdateLife;
    public static event Action OnPlayerDie;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;

    private int _health;

    private void Start()
    {
        _health = _maxHealth;
        OnUpdateLife?.Invoke(_health, _maxHealth);
    }

    private void OnEnable()
    {
        CollisionController.OnPlayerCrashed += OnPlayerCrashed_ReduceHealth;
    }

    private void OnDisable()
    {
        CollisionController.OnPlayerCrashed -= OnPlayerCrashed_ReduceHealth;
    }

    private void OnPlayerCrashed_ReduceHealth()
    {
        _health -= _damage;
        if (_health <= 0)
        {
            _health = 0;
            OnPlayerDie?.Invoke();
        }
        OnUpdateLife?.Invoke(_health, _maxHealth);
    }
}
