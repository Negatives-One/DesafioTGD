using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject opt;
    public GameObject self;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Options()
    {
        self.SetActive(false);
        opt.SetActive(true);
    }

    public void Back()
    {
        self.SetActive(true);
        opt.SetActive(false);
    }
}