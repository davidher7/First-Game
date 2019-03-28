using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    int currentSceneIndex;

    public Animator animator;

    public void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    public void NextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);

    }

    public void Back()
    {
        // Deletes current position (Player) before calling LoadPlayerPos
        PlayerPrefs.DeleteKey(currentSceneIndex + "PlayerX");
        PlayerPrefs.DeleteKey(currentSceneIndex + "PlayerY");

        SceneManager.LoadScene(currentSceneIndex - 1);
        
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("QUIT GAME!");

    }

    // Checks which door the player collides with and calls the appropriate function
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.gameObject.tag == "Player" && transform.position.x > 0)
        {
            FadeOut();
            SetPlayerPos();
            NextScene();

        }

        if (collision.gameObject.tag == "Player" && transform.position.x < 0)
        {
            FadeOut();
            Back();
            
        }
    }

    void SetPlayerPos()
    {
        // Setting PlayerPrefs' Keys and Float
        PlayerPrefs.SetFloat(currentSceneIndex + "PlayerX", 2.5f);
        PlayerPrefs.SetFloat(currentSceneIndex + "PlayerY", -1.685f);

        Debug.Log("Save");
        
    }

    public void FadeOut()
    {
        Debug.Log("FadeOut");
        animator.SetTrigger("FadeOut");

    }
}
