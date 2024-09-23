using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class MeleeStateMachine : SimpleStateMachine
{
    public IdleState idle;
    public MoveInRangeState moveInRange;
    public AttackState melee;
    
    public bool isAlive;
    public float inAttackRange = 1.0f;

    public Transform target;
    private Health health;

    void Awake()
    {
        states.Add(idle);
        states.Add(moveInRange);
        states.Add(melee);

        foreach (SimpleState s in states)
        {
            s.stateMachine = this;
        }
    }

    void Start()
    {
        health = gameObject.GetComponent<Health>();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;

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
