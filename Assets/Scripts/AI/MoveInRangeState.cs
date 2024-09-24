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

        // Use 'as' to safely cast the state machine and check for null
        RangeStateMachine rangeSM = stateMachine as RangeStateMachine;
        MeleeWeaponStateMachine meleeWeaponSM = stateMachine as MeleeWeaponStateMachine;
        MeleeStateMachine meleeSM = stateMachine as MeleeStateMachine;

        if (rangeSM != null)
        {
            agent = rangeSM.GetComponent<NavMeshAgent>();
            attackRange = rangeSM.inAttackRange + 5.0f;
        }
        else if (meleeWeaponSM != null)
        {
            agent = meleeWeaponSM.GetComponent<NavMeshAgent>();
            attackRange = meleeWeaponSM.inAttackRange + 0.5f;
        }
        else if (meleeSM != null)
        {
            agent = meleeSM.GetComponent<NavMeshAgent>();
            attackRange = meleeSM.inAttackRange + 0.5f;
        }
    }

    public override void UpdateState(float dt)
    {
        // Again, safely cast the state machine using 'as'
        MeleeStateMachine meleeSM = stateMachine as MeleeStateMachine;
        MeleeWeaponStateMachine meleeWeaponSM = stateMachine as MeleeWeaponStateMachine;
        RangeStateMachine rangeSM = stateMachine as RangeStateMachine;

        if (meleeSM != null && meleeSM.isAlive)
        {
            agent.SetDestination(meleeSM.target.transform.position);

            if (Vector3.Distance(agent.transform.position, meleeSM.target.transform.position) < attackRange)
            {
                stateMachine.ChangeState(nameof(AttackState));
            }
        }

        if (meleeWeaponSM != null && meleeWeaponSM.isAlive)
        {
            agent.SetDestination(meleeWeaponSM.target.transform.position);

            if (Vector3.Distance(agent.transform.position, meleeWeaponSM.target.transform.position) < attackRange)
            {
                stateMachine.ChangeState(nameof(AttackState));
            }
        }

        if (rangeSM != null && rangeSM.isAlive)
        {
            agent.SetDestination(rangeSM.target.transform.position);

            if (Vector3.Distance(agent.transform.position, rangeSM.target.transform.position) < attackRange)
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
