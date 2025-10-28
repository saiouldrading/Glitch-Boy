using UnityEditor;
using UnityEngine;

public class Script : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject PauseMenuUi;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Pause()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Resume()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }


    public void LoadMenu()
    {
        Debug.Log("Options open");
    }


    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("game cut jao");
    }
}
