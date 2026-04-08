using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Npc/NpcData")]

public class StatsDataSO : ScriptableObject
{
    public int level;

    [Header("Stats")]
    public int life;
    public float speed;

    [Header("Enemy")]
    public float chanceToBeEnemy;

    [Header("Player")]
    public float movementSpeedHor;
    public float movementSpeedVer;
    public float maxSpeed;

    [Header("Attacking")]
    public float distanceToShoot;
    public float shootingSpeed;
    public int shootingDamage;
}