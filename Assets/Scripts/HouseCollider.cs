using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseCollider : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName = "Level2";
    
    [Header("Transition Settings")]
    [SerializeField] private float blackScreenDuration = 1f;
    
    [Header("Sound Settings")]
    [SerializeField] private AudioClip transitionSound; // Sound clip
    [SerializeField] private float soundVolume = 1f;
    
    [Header("References")]
    [SerializeField] private GameObject blackScreen;
    
    private bool hasTriggered = false;
    private AudioSource audioSource;

    void Start()
    {
        // Black screen setup
        if (blackScreen == null)
        {
            blackScreen = GameObject.Find("BlackScreen");
        }
        
        if (blackScreen != null)
        {
            blackScreen.SetActive(false);
        }
        
        // AudioSource setup - pehle existing AudioSource check karo
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Agar nahi hai to naya banayo
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // AudioSource settings force karo
        audioSource.mute = false; // Mute off
        audioSource.playOnAwake = false; // Play on awake off
        audioSource.volume = soundVolume; // Volume set
        audioSource.priority = 128; // Normal priority
        
        Debug.Log("AudioSource setup complete - Mute: " + audioSource.mute);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartTransition();
        }
    }
    
    void StartTransition()
    {
        // Sound play karo
        PlayTransitionSound();
        
        // Black screen show karo
        ShowBlackScreen();
        
        // Scene load karo after delay
        Invoke("LoadNextScene", blackScreenDuration);
    }
    
    void PlayTransitionSound()
    {
        if (transitionSound != null)
        {
            if (audioSource != null)
            {
                // Directly play karo without AudioSource settings par depend kare
                audioSource.mute = false;
                audioSource.volume = soundVolume;
                audioSource.PlayOneShot(transitionSound);
                Debug.Log("Transition sound played: " + transitionSound.name);
            }
            else
            {
                // Alternative method
                AudioSource.PlayClipAtPoint(transitionSound, transform.position, soundVolume);
                Debug.Log("Transition sound played using AudioSource.PlayClipAtPoint");
            }
        }
        else
        {
            Debug.LogWarning("Transition sound not assigned!");
        }
    }
    
    void ShowBlackScreen()
    {
        if (blackScreen != null)
        {
            blackScreen.SetActive(true);
        }
        else
        {
            CreateBlackScreen();
        }
    }
    
    void CreateBlackScreen()
    {
        GameObject blackScreenObj = new GameObject("BlackScreen");
        Canvas canvas = blackScreenObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        
        UnityEngine.UI.Image bg = blackScreenObj.AddComponent<UnityEngine.UI.Image>();
        bg.color = Color.black;
        
        DontDestroyOnLoad(blackScreenObj);
        blackScreen = blackScreenObj;
    }
    
    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set!");
        }
    }
    
    void OnDrawGizmos()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, collider.bounds.size);
        }
    }
}