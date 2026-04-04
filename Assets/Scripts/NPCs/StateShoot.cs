using UnityEngine;
using UnityEngine.AI;

public class StateShoot : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController npcPatrol, NavMeshAgent agent)
    {
        base.Initialize(animator, rigidbody, npcPatrol, agent);
        state = StateType.Shoot;
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_agent.isOnNavMesh)
            _agent.isStopped = true;
        _anim.SetInteger(_state, (int)state);
    }

    public override void OnUpdate()
    {
        _patrol.EnableShooting(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        _patrol.EnableShooting(false);
        _agent.isStopped = false;
    }
}