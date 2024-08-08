using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // using this when build game
        // Application.Quit();
        // comment this when build game
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }
}
 