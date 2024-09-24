using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    public LoseMenu loseMenu;
    public WinMenu winMenu;

    // Lists to store objects with enemy and friendly tags
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> friendlies = new List<GameObject>();

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Find all objects with the "Red" tag and add them to the enemies list
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.AddRange(enemyArray);

        // Find all objects with the "Blue" tag and add them to the friendlies list
        GameObject[] friendlyArray = GameObject.FindGameObjectsWithTag("Friendly");
        friendlies.AddRange(friendlyArray);

        //ToggleStateMachines(false);
        PlacingPhase();
    }

    // Function to activate or deactivate all state machines
    // private void ToggleStateMachines(bool state)
    // {
    //     // Toggle for friendlies
    //     foreach (GameObject friendly in friendlies)
    //     {
    //         if (friendly.TryGetComponent(out RangeStateMachine rangeSM))
    //             rangeSM.enabled = state;
    //         if (friendly.TryGetComponent(out MeleeStateMachine meleeSM))
    //             meleeSM.enabled = state;
    //         if (friendly.TryGetComponent(out MeleeWeaponStateMachine meleeWeaponSM))
    //             meleeWeaponSM.enabled = state;
    //     }

    //     // Toggle for enemies
    //     foreach (GameObject enemy in enemies)
    //     {
    //         if (enemy.TryGetComponent(out RangeStateMachine rangeSM))
    //             rangeSM.enabled = state;
    //         if (enemy.TryGetComponent(out MeleeStateMachine meleeSM))
    //             meleeSM.enabled = state;
    //         if (enemy.TryGetComponent(out MeleeWeaponStateMachine meleeWeaponSM))
    //             meleeWeaponSM.enabled = state;
    //     }
    // }

    public void PlacingPhase()
    {
        Time.timeScale = 0.0f;
    }

    // Function is called when the fight button is pressed
    public void ActivateRound()
    {
        Time.timeScale = 1.0f;
        //ToggleStateMachines(true);
    }

    // Function to add object to corresponding list based on tag
    public void AddObjectToList(GameObject obj)
    {
        if (obj.CompareTag("Enemy"))
        {
            enemies.Add(obj);
        }
        else if (obj.CompareTag("Friendly"))
        {
            friendlies.Add(obj);
        }
    }

    // Function to remove object from corresponding list based on tag
    public void RemoveObjectFromList(GameObject obj)
    {
        if (obj.CompareTag("Red"))
        {
            enemies.Remove(obj);
            CheckWinCondition();
        }
        else if (obj.CompareTag("Blue"))
        {
            friendlies.Remove(obj);
            CheckLoseCondition();
        }
    }

    // Function to check lose condition
    private void CheckLoseCondition()
    {
        if (friendlies.Count == 0)
        {
            Debug.Log("Your Team Lost");
            loseMenu.Lose();
        }
    }

    private void CheckWinCondition()
    {
        if (enemies.Count == 0)
        {
            Debug.Log("Your Team Won");
            winMenu.Win();
        }
    }
}
