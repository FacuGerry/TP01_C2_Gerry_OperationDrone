using System;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public static event Action<int> OnPlayerCrashed;

    private PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnPlayerCrashed?.Invoke(CalculateDamage(_controller._rb.linearVelocity.magnitude));
        Debug.Log("player collided");
    }

    private int CalculateDamage(float speed)
    {
        int damage = 0;
        if (speed < (_controller.readableMaxSpeed / 3f))
        {
            damage = 0;
        }
        else if (speed >= (_controller.readableMaxSpeed / 3f) && speed < ((_controller.readableMaxSpeed / 3f) * 2f))
        {
            damage = 5;
        }
        else if (speed >= ((_controller.readableMaxSpeed / 3f) * 2f))
        {
            damage = 10;
        }

        return damage;
    }

}
