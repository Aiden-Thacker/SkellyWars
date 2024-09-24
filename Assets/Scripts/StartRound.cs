using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound : MonoBehaviour
{
    public GameObject[] friendlies;
    public GameObject[] enemies;

    void Start()
    {
        // Get all friendlies and enemies in the scene by their tags
        friendlies = GameObject.FindGameObjectsWithTag("Friendly");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void ActivateRound()
    {
        // Activate all friendlies
        foreach (GameObject friendly in friendlies)
        {
            // Check for and activate the correct state machine on each friendly
            if (friendly.TryGetComponent(out RangeStateMachine rangeSM))
            {
                rangeSM.startMatch = true;
            }
            if (friendly.TryGetComponent(out MeleeStateMachine meleeSM))
            {
                meleeSM.startMatch = true;
            }
            if (friendly.TryGetComponent(out MeleeWeaponStateMachine meleeWeaponSM))
            {
                meleeWeaponSM.startMatch = true;
            }
        }

        // Activate all enemies
        foreach (GameObject enemy in enemies)
        {
            // Check for and activate the correct state machine on each enemy
            if (enemy.TryGetComponent(out RangeStateMachine rangeSM))
            {
                rangeSM.startMatch = true;
            }
            if (enemy.TryGetComponent(out MeleeStateMachine meleeSM))
            {
                meleeSM.startMatch = true;
            }
            if (enemy.TryGetComponent(out MeleeWeaponStateMachine meleeWeaponSM))
            {
                meleeWeaponSM.startMatch = true;
            }
        }
    }
}
