using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class MeleeDamage : MonoBehaviour
{
    public int dmg;
    public Health enemyHealth;

    //public AttackState attackState;

    private void Start()
    {
        if (enemyHealth == null)
        {
            enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        }
    }
    public void DealDamage()
    {
        Debug.Log("Enemy attacking");
        enemyHealth.Damage(dmg);
    }
}
