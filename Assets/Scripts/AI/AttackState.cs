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
    NavMeshAgent agent;
    private bool playerInRange;
    public bool isAttacking;

    public override void OnStart()
    {
        Debug.Log("Attack State");
        base.OnStart();

        if (stateMachine is RangeStateMachine)
        {
            agent = ((RangeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agent.SetDestination(((RangeStateMachine)stateMachine).transform.position);
        }


        if (stateMachine is MeleeWeaponStateMachine)
        {
            agent = ((MeleeWeaponStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agent.SetDestination(((MeleeWeaponStateMachine)stateMachine).transform.position);
        }

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agent.SetDestination(((MeleeStateMachine)stateMachine).transform.position);
        }

        time.StartTimer(2, true);
        if (attack == null)
            attack = new UnityEvent();
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is RangeStateMachine)
        {
            ((RangeStateMachine)stateMachine).transform.LookAt(((RangeStateMachine)stateMachine).target);
            if (!isAttacking)
            {
                Debug.Log("Attacking");
                isAttacking = true;
                attack.Invoke();

                isAttacking = false;
            }
        }
        if (stateMachine is MeleeStateMachine)
        {
            ((MeleeStateMachine)stateMachine).transform.LookAt(((MeleeStateMachine)stateMachine).target);
            if (!isAttacking)
            {
                Debug.Log("Attacking");
                isAttacking = true;
                attack.Invoke();

                isAttacking = false;
            }
        }
        if (stateMachine is MeleeWeaponStateMachine)
        {
            ((MeleeWeaponStateMachine)stateMachine).transform.LookAt(((MeleeWeaponStateMachine)stateMachine).target);
            if (!isAttacking)
            {
                Debug.Log("Attacking");
                isAttacking = true;
                attack.Invoke();

                isAttacking = false;
            }
        }





    }
    
    public override void OnExit()
    {
        base.OnExit();


    }
}