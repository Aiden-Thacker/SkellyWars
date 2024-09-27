using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SuperPupSystems.Helper;

public class TutorialGameManager : MonoBehaviour
{
    //public static GameManager instance; 

    //public LoseMenu loseMenu;
    //public WinMenu winMenu;

    // Lists to store objects with enemy and friendly tags
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> friendlies = new List<GameObject>();

    private void Awake()
    {
        // Singleton pattern
        // if (instance == null)
        // {
        //     instance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        //     return;
        // }
    }

    private void Start()
    {
        //PlacingPhase();
    }

    public void PlacingPhase()
    {
        Time.timeScale = 0.0f;
    }

    // Function is called when the fight button is pressed
    public void ActivateRound()
    {
        Time.timeScale = 1.0f;
        // Find all objects with the "Red" tag and add them to the enemies list
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyArray)
        {
            AddObjectToList(enemy);
        }

        // Find all objects with the "Blue" tag and add them to the friendlies list
        GameObject[] friendlyArray = GameObject.FindGameObjectsWithTag("Friendly");
        foreach (GameObject friendly in friendlyArray)
        {
            AddObjectToList(friendly);
        }
    }

    // Function to add object to corresponding list based on tag
    public void AddObjectToList(GameObject obj)
    {
        Debug.Log("Adding object to list: " + obj.name);
        if (obj.CompareTag("Enemy"))
        {
            enemies.Add(obj);
            Health health = obj.GetComponent<Health>();
            if (health != null)
            {
                Debug.Log("Enemy health component found and listener added.");
                health.outOfHealth.AddListener(() => RemoveObjectFromList(obj));
            }
        }
        else if (obj.CompareTag("Friendly"))
        {
            friendlies.Add(obj);
            Health health = obj.GetComponent<Health>();
            if (health != null)
            {
                Debug.Log("Friendly health component found and listener added.");
                health.outOfHealth.AddListener(() => RemoveObjectFromList(obj));
            }
        }
    }

    // Function to remove object from corresponding list based on tag
    public void RemoveObjectFromList(GameObject obj)
    {
        if (obj.CompareTag("Enemy"))
        {
            enemies.Remove(obj);
            CheckWinCondition();
        }
        else if (obj.CompareTag("Friendly"))
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
            //loseMenu.Lose();
        }
    }

    private void CheckWinCondition()
    {
        if (enemies.Count == 0)
        {
            Debug.Log("Your Team Won");
            //winMenu.Win();
        }
    }
}
