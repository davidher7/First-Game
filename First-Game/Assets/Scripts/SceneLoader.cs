using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void nextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

    }

    public void startMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();

    }
}
