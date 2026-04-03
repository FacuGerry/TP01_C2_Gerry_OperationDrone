using UnityEngine;
using UnityEngine.AI;

public class StateWalk : EnemyStates
{
    private int _index = 0;

    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController npcPatrol, NavMeshAgent agent)
    {
        base.Initialize(animator, rigidbody, npcPatrol, agent);
        state = StateType.Walk;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _agent.SetDestination(_patrol.positions[_index]);
    }

    public override void OnUpdate()
    {
        if (_rb.position.x == _patrol.positions[_index].x && _rb.position.z == _patrol.positions[_index].z)
        {
            _index++;
            if (_index > _patrol.positions.Count - 1)
                _index = 0;
            _agent.SetDestination(_patrol.positions[_index]);
        }
    }
}
