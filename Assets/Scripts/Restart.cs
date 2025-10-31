using UnityEngine;

public class Restart : MonoBehaviour
{
    public static Restart Instance;
    public GameObject restartUi;
    public AudioClip BM;
    private AudioSource audioSource;
    public AudioClip buttonclick;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    void Awake()
    {
        Instance = this;
        if (restartUi != null)
            restartUi.SetActive(false);
    }

    public void ShowRestartUI()
    {
        if (restartUi != null)
            restartUi.SetActive(true);
        audioSource.Pause();
    }

    public void HideRestartUI()
    {
        if (restartUi != null)
            restartUi.SetActive(false);
        audioSource.Stop();
        audioSource.Play();

    }

    public void OnRestartButtonClicked()
    {
        Debug.Log("Restart button clicked!");
        audioSource.PlayOneShot(buttonclick);


        MainPlayer player = FindFirstObjectByType<MainPlayer>();
        if (player != null)
        {
            Debug.Log("Player found, calling Respawn.");
            player.Respawn();
        }
        else
        {
            Debug.Log("Player not found.");
        }

        HideRestartUI();
    }
}
