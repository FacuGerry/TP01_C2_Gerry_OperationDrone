using UnityEngine;

public class BuleltsCollision : MonoBehaviour
{
    [SerializeField] private int _damageBullet;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out NpcHealthSystem npc))
            npc.OnBulletShot_TakeDamage(_damageBullet);

        gameObject.SetActive(false);
    }
}