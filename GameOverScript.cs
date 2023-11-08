using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Scene1");
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

