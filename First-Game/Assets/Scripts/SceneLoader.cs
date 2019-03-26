using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public GameObject player;
    int currentSceneIndex;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }
    private void nextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);

    }

    private void back()
    {
        PlayerPrefs.DeleteKey(currentSceneIndex + "PlayerX");
        PlayerPrefs.DeleteKey(currentSceneIndex + "PlayerY");

        SceneManager.LoadScene(currentSceneIndex - 1);
        
    }

    private void startMenu()
    {
        SceneManager.LoadScene(0);

    }

    private void QuitGame()
    {
        Application.Quit();

        Debug.Log("QUIT GAME!");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.gameObject.tag == "Player" && transform.position.x > 0)
        {
            SetPlayerPos();
            nextScene();

        }

        if (collision.gameObject.tag == "Player" && transform.position.x < 0)
        {
            back();
            

        }
    }

    void SetPlayerPos()
    {
        //Saving
        PlayerPrefs.SetFloat(currentSceneIndex + "PlayerX", 2.5f);
        PlayerPrefs.SetFloat(currentSceneIndex + "PlayerY", -1.685f);

        Debug.Log("Save");
        
    }

    
}
