using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour{

	public void LoadLevel(string level_name)
    {
        SceneManager.LoadScene(level_name);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Application.Quit() called.");
    }
}
