using UnityEngine;
using UnityEngine.AI;

public class StateShoot : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController npcPatrol, NavMeshAgent agent)
    {
        base.Initialize(animator, rigidbody, npcPatrol, agent);
        state = StateType.Shoot;
    }

    public override void OnUpdate()
    {

    }
}