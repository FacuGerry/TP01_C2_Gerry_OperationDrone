using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Npc/NpcData")]

public class NpcDataSO : ScriptableObject
{
    public int life;
    public float speed;
    public int distanceToShoot;
    public int chanceToBeEnemy;
}