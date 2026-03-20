using System;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public static event Action OnPlayerCrashed;

    private void OnCollisionEnter(Collision collision)
    {
        OnPlayerCrashed?.Invoke();
        Debug.Log("player collided");
    }

}
