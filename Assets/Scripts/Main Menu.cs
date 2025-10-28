using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMEnuScript : MonoBehaviour
{

    public AudioClip click;
    private AudioSource audioSource;
    

    public void Start()
    {
        audioSource= GetComponent<AudioSource>();
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
