using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject PauseMenuUi;
    public GameObject OptionsMenuUi;
    public Slider volumeSlider;
    public AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }
    void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }

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
        OptionsMenuUi.SetActive(false);
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

    public void BackToMenu()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
        OptionsMenuUi.SetActive(false);

    }
    public void OptionsMenu()
    {
        PauseMenuUi.SetActive(false);
        OptionsMenuUi.SetActive(true);
    }
}
