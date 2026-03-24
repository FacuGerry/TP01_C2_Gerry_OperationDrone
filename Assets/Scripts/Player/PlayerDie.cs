using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private void OnEnable()
    {
        HealthSystem.OnPlayerDie += OnPlayerDie_SetToInactive;
    }

    private void OnDisable()
    {
        HealthSystem.OnPlayerDie -= OnPlayerDie_SetToInactive;
    }

    private void OnPlayerDie_SetToInactive()
    {
        gameObject.SetActive(false);
    }
}
