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
        OnPlayerCrashed?.Invoke((int)collision.relativeVelocity.magnitude);
        Debug.Log("player collided");
    }
}
