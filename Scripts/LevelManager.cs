using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Text gameText; 
    public string nextLevel;
    public static bool isGameOver = false;
    public AudioClip gameOverSFX;
    //public AudioClip gameWonSFX;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelWon()
    {
        isGameOver = true;
        gameText.text = "YOU WIN!";
        gameText.gameObject.SetActive(true);

        //AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        FindObjectOfType<robotBehavior>().Win();

        if(!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 4);
        }
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER";
        gameText.gameObject.SetActive(true);

        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", 3);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
