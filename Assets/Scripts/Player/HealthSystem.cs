using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static event Action<int> OnUpdateLife;
    public static event Action OnPlayerDie;

    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private void Start()
    {
        OnUpdateLife?.Invoke(_health);
    }

    private void OnEnable()
    {
        CollisionController.OnPlayerCrashed += OnPlayerCrashed_ReduceHealth;
    }

    private void OnDisable()
    {
        CollisionController.OnPlayerCrashed -= OnPlayerCrashed_ReduceHealth;
    }

    private void CheckLife()
    {
        if (_health <= 0)
        {
            OnPlayerDie?.Invoke();
        }
    }

    private void OnPlayerCrashed_ReduceHealth()
    {
        _health -= _damage;
        OnUpdateLife?.Invoke(_health);
        CheckLife();
    }
}
