using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class AttackState : SimpleState
{
    public Timer time;
    public UnityEvent attack;
    public UnityEvent stopAttacking;
    private NavMeshAgent agent;
    public bool isAttacking;
    private GameObject target;

    private float attackRange;
    private float recheckTimer = 1.0f;
    private bool targetInRange;

    public override void OnStart()
    {
        Debug.Log("Attack State");
        base.OnStart();

        if (stateMachine is RangeStateMachine rangeSM)
        {
            agent = rangeSM.GetComponent<NavMeshAgent>();
            target = rangeSM.target;
            attackRange = rangeSM.inAttackRange;
            agent.SetDestination(rangeSM.transform.position);
            agent.updateRotation = false;
        }
        else if (stateMachine is MeleeWeaponStateMachine meleeWeaponSM)
        {
            agent = meleeWeaponSM.GetComponent<NavMeshAgent>();
            target = meleeWeaponSM.target;
            attackRange = meleeWeaponSM.inAttackRange;
            agent.SetDestination(meleeWeaponSM.transform.position);
        }
        else if (stateMachine is MeleeStateMachine meleeSM)
        {
            agent = meleeSM.GetComponent<NavMeshAgent>();
            target = meleeSM.target;
            attackRange = meleeSM.inAttackRange;
            agent.SetDestination(meleeSM.transform.position);
        }

        // Start the attack cooldown timer
        if (time != null)
        {
            time.StartTimer(2.0f, true);
            time.timeout.AddListener(PerformAttack);
        }

        if (attack == null)
        {
            attack = new UnityEvent();
        }
    }

    public override void UpdateState(float dt)
    {
        recheckTimer -= dt;
        if (recheckTimer > 0) 
        {
            return;
        }
        recheckTimer = 1.0f;  // Reset the timer

        // Check if the target is dead or out of range
        if (target == null || !IsTargetAlive() || !IsTargetInRange())
        {
            Debug.Log("Finding a new target...");
            stateMachine.ChangeState(nameof(MoveInRangeState));
            return;
        }

        // Smoothly rotate toward the target when in range
        if (IsTargetInRange())
        {
            Vector3 direction = (target.transform.position - agent.transform.position).normalized;
            direction.y = 0;  // Ensure the AI only rotates along the Y-axis
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Let the timer control when the attack happens
        if (time.timeLeft <= 0 && !isAttacking)
        {
            PerformAttack();  // Attack if the timer has finished
        }
    }

    // Perform the attack logic
    private void PerformAttack()
    {
        if (!isAttacking)
        {
            Debug.Log("Performing attack!");
            isAttacking = true;
            attack.Invoke();
            isAttacking = false;
            time.StartTimer();
        }
    }

    // Check if the target is alive
    private bool IsTargetAlive()
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null && targetHealth.currentHealth == 0)
        {
            return false;
        }
        return true;
    }

    // Check if the target is within attack range
    private bool IsTargetInRange()
    {
        float buffer = 0.5f; // Buffer zone to prevent immediate state switching
        if (Vector3.Distance(agent.transform.position, target.transform.position) <= attackRange + buffer)
        {
            return true;
        }
        return false;
    }

    public override void OnExit()
    {
        base.OnExit();
        if (time != null)
        {
            time.StopTimer();  
            time.timeout.RemoveListener(PerformAttack);  
        }
    }
}
