using SuperPupSystems.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class MoveInRangeState : SimpleState
{
    private NavMeshAgent agent;
    private float attackRange;

    public override void OnStart()
    {
        Debug.Log("Move State");
        base.OnStart();

        if (stateMachine is RangeStateMachine)
        {
            agent = ((RangeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            attackRange = ((RangeStateMachine)stateMachine).inAttackRange + 5.0f;
        }

        if (stateMachine is MeleeWeaponStateMachine)
        {
            agent = ((MeleeWeaponStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            attackRange = ((MeleeWeaponStateMachine)stateMachine).inAttackRange + 1.0f;
        }

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            attackRange = ((MeleeStateMachine)stateMachine).inAttackRange + 1.0f;
        }
        
        
    }

    public override void UpdateState(float dt)
    {
        if (((MeleeStateMachine)stateMachine).isAlive)
        {
            agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
            
            if(Vector3.Distance(agent.transform.position, ((MeleeStateMachine)stateMachine).target.position) < attackRange)
            {
                stateMachine.ChangeState(nameof(AttackState));
            }
        }

        if (((MeleeWeaponStateMachine)stateMachine).isAlive)
        {
            agent.SetDestination(((MeleeWeaponStateMachine)stateMachine).target.position);
            
            if(Vector3.Distance(agent.transform.position, ((MeleeWeaponStateMachine)stateMachine).target.position) < attackRange)
            {
                stateMachine.ChangeState(nameof(AttackState));
            }
        }

        if (((RangeStateMachine)stateMachine).isAlive)
        {
            agent.SetDestination(((RangeStateMachine)stateMachine).target.position);
            
            if(Vector3.Distance(agent.transform.position, ((RangeStateMachine)stateMachine).target.position) < attackRange)
            {
                stateMachine.ChangeState(nameof(AttackState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
