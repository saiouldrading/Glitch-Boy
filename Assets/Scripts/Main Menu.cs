using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMEnuScript : MonoBehaviour
{

    public AudioClip click;
    public AudioSource audioSource;
    public Slider volumeSlider;


    public void Start()
    {

        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

    }
    void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        audioSource.PlayOneShot(click);

    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(click);
        Application.Quit();
        Debug.Log("QuitGame");
    }

}
