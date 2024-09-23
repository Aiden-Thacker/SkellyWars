using System.Collections;
using System.Collections.Generic;
using SuperPupSystems.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class IdleState : SimpleState
{
    public string targetFoundState;
    private GameObject target;
    private GameObject gameObject;
    private TargetingSystem targetingSystem;
    public override void OnStart()
    {
        gameObject = stateMachine.gameObject;
        targetingSystem = gameObject.GetComponent<TargetingSystem>();
    }

    public override void UpdateState(float _dt)
    {
        // Find target
        target = targetingSystem.FindTarget();

        if (target == false)
            return;

        stateMachine.ChangeState(targetFoundState);
    }

    public override void OnExit()
    {

    }
}
