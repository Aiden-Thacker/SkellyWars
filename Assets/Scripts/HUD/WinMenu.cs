using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public bool didWin = false;
    public GameObject winSection;


    void Start()
    {
        winSection.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Win()
    {
        Debug.Log("You have WON!");
        didWin = true;
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        winSection.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        //SceneManager.LoadSceneAysnc(1);
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void QuitGame()
    {
        #if(UNITY_EDITOR)
        Debug.Log("Quiting Play Mode");
        EditorApplication.ExitPlaymode();
        #else
        Debug.Log("Quitting Build");
        Application.Quit();
        #endif
    }
}
