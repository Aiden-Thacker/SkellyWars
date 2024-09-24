using SuperPupSystems.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class MoveInRangeState : SimpleState
{
    private NavMeshAgent agent;
    private float attackRange;
    private TargetingSystem targetingSystem;
    private GameObject target;

    public override void OnStart()
    {
        Debug.Log("Move State");
        base.OnStart();

        // Initialize the agent and attack range based on the state machine
        if (stateMachine is RangeStateMachine rangeSM)
        {
            agent = rangeSM.GetComponent<NavMeshAgent>();
            attackRange = rangeSM.inAttackRange + 5.0f;
            targetingSystem = rangeSM.GetComponent<TargetingSystem>();
            target = rangeSM.target;
        }
        else if (stateMachine is MeleeWeaponStateMachine meleeWeaponSM)
        {
            agent = meleeWeaponSM.GetComponent<NavMeshAgent>();
            attackRange = meleeWeaponSM.inAttackRange + 0.5f;
            targetingSystem = meleeWeaponSM.GetComponent<TargetingSystem>();
            target = meleeWeaponSM.target;
        }
        else if (stateMachine is MeleeStateMachine meleeSM)
        {
            agent = meleeSM.GetComponent<NavMeshAgent>();
            attackRange = meleeSM.inAttackRange + 0.5f;
            targetingSystem = meleeSM.GetComponent<TargetingSystem>();
            target = meleeSM.target;
        }
    }

    public override void UpdateState(float dt)
    {
        // Safely cast the state machine
        if (stateMachine is MeleeStateMachine meleeSM && meleeSM.isAlive)
        {
            UpdateTarget(meleeSM);  // Continuously update target
            MoveTowardsTarget(meleeSM);
        }
        else if (stateMachine is MeleeWeaponStateMachine meleeWeaponSM && meleeWeaponSM.isAlive)
        {
            UpdateTarget(meleeWeaponSM);
            MoveTowardsTarget(meleeWeaponSM);
        }
        else if (stateMachine is RangeStateMachine rangeSM && rangeSM.isAlive)
        {
            UpdateTarget(rangeSM);
            MoveTowardsTarget(rangeSM);
        }
    }

    private void UpdateTarget(SimpleStateMachine sm)
    {
        // Re-acquire target if null or out of range
        if (target == null || Vector3.Distance(agent.transform.position, target.transform.position) > attackRange)
        {
            target = targetingSystem.FindTarget();
            if (sm is MeleeStateMachine meleeSM) 
            {
                meleeSM.target = target;
            }
            if (sm is MeleeWeaponStateMachine meleeWeaponSM) 
            {
                meleeWeaponSM.target = target;
            }
            if (sm is RangeStateMachine rangeSM) 
            {
                rangeSM.target = target;
            }
        }
    }

    private void MoveTowardsTarget(SimpleStateMachine sm)
    {
        // Continue moving toward the target
        if (target != null)
        {
            agent.SetDestination(target.transform.position);

            // Check if in attack range and switch to attack state
            if (Vector3.Distance(agent.transform.position, target.transform.position) < attackRange)
            {
                stateMachine.ChangeState(nameof(AttackState));
            }
        }
        else
        {
            // No target found, go back to idle or another fallback state
            stateMachine.ChangeState(nameof(IdleState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
