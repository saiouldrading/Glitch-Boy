using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject PauseMenuUi;
    public GameObject OptionsMenuUi;
    public Slider volumeSlider;
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceSFX;

    private void Start()
    {


        // Add listener to slider
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float value)
    {

        // Control both BGM & SFX
        audioSourceBGM.volume = value;
        audioSourceSFX.volume = value;

        // Optional: save value
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    private void OnEnable()
    {
        // Load saved volume if available
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        if (audioSourceBGM != null) audioSourceBGM.volume = savedVolume;
        if (audioSourceSFX != null) audioSourceSFX.volume = savedVolume;
        if (volumeSlider != null) volumeSlider.value = savedVolume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused) Resume();
            else Pause();
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
        Debug.Log("Game quit");
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
