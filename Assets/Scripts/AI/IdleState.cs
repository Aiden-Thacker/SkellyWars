using System.Collections;
using System.Collections.Generic;
using SuperPupSystems.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class IdleState : SimpleState
{
    private GameObject target;
    private TargetingSystem targetingSystem;
    private UnityEngine.AI.NavMeshAgent agent;
    public override void OnStart()
    {
        Debug.Log("Idle State");
        base.OnStart();

        if (stateMachine is RangeStateMachine rangeSM)
        {
            agent = rangeSM.GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
        else if (stateMachine is MeleeWeaponStateMachine meleeWeaponSM)
        {
            agent = meleeWeaponSM.GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
        else if (stateMachine is MeleeStateMachine meleeSM)
        {
            agent = meleeSM.GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        // Initialize targeting system (if not done elsewhere)
        if (targetingSystem == null)
        {
            targetingSystem = stateMachine.GetComponent<TargetingSystem>();
        }
    }

    public override void UpdateState(float _dt)
    {
        // Check if the match has started before doing anything else
        bool matchStarted = false;

        if (stateMachine is RangeStateMachine rangeSM && rangeSM.startMatch)
        {
            matchStarted = true;
        }
        else if (stateMachine is MeleeWeaponStateMachine meleeWeaponSM && meleeWeaponSM.startMatch)
        {
            matchStarted = true;
        }
        else if (stateMachine is MeleeStateMachine meleeSM && meleeSM.startMatch)
        {
            matchStarted = true;
        }

        // If the match hasn't started yet, return early and stay in idle
        if (!matchStarted) return;

        // Find a target only if the match has started
        target = targetingSystem.FindTarget();

        if (target == null) return;

        // If a target is found, switch to the target found state or attack state
        stateMachine.ChangeState(nameof(MoveInRangeState));
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
