using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class MeleeWeaponStateMachine : SimpleStateMachine
{
    public IdleState idle;
    public MoveInRangeState moveInRange;
    public AttackState meleeWeapon;
    
    public bool isAlive;
    public bool startMatch = false;
    public float inAttackRange = 1.0f;
    public TargetingSystem targetingSystem;
    public GameObject target;

    private Health health;

    void Awake()
    {
        states.Add(idle);
        states.Add(moveInRange);
        states.Add(meleeWeapon);

        foreach (SimpleState s in states)
        {
            s.stateMachine = this;
        }
    }

    void Start()
    {
        health = gameObject.GetComponent<Health>();

        targetingSystem = gameObject.GetComponent<TargetingSystem>();

        target = targetingSystem.FindTarget();

        ChangeState(nameof(MoveInRangeState));
    }

    void Update()
    {
        if(health.currentHealth > 0)
        {
            isAlive = true;
        }else
        {
            isAlive = false;
        }
    }
}
