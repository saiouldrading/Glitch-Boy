using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HouseCollider : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName = "MineMenu";
    public GameObject YouwinUI;

    [Header("Transition Settings")]
    [SerializeField] private float blackScreenDuration = 1.5f;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip transitionSound;
    [SerializeField] private float soundVolume = 1f;

    [Header("References")]
    [SerializeField] private GameObject blackScreenCanvas; // Must have Image component

    private bool hasTriggered = false;
    private AudioSource audioSource;

    void Start()
    {
        if (YouwinUI != null)
            YouwinUI.SetActive(false);

        // AudioSource setup
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.volume = soundVolume;
        audioSource.mute = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(TransitionSequence());
        }
    }

    IEnumerator TransitionSequence()
    {
        // Step 1: Stop player movement and fade out sound
        MainPlayer.Instance.StopMovement();
        MainPlayer.Instance.FadeSound(3f);

        // Step 2: Show "You Win" UI
        if (YouwinUI != null)
            YouwinUI.SetActive(true);

        // Step 3: Play transition sound
        PlayTransitionSound();

        // Step 4: Wait before black screen
        yield return new WaitForSeconds(2f);

        // Step 5: Fade in black screen
        if (blackScreenCanvas != null)
        {
            blackScreenCanvas.SetActive(true);
            UnityEngine.UI.Image img = blackScreenCanvas.GetComponent<UnityEngine.UI.Image>();
            if (img != null)
                yield return StartCoroutine(FadeImage(img, 0f, 1f, blackScreenDuration));
        }

        // Step 6: Small pause for effect
        yield return new WaitForSeconds(0.5f);

        // Step 7: Load next scene
        LoadNextScene();
    }

    void PlayTransitionSound()
    {
        if (transitionSound != null)
        {
            audioSource.PlayOneShot(transitionSound);
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty("MineMenu"))
        {
            SceneManager.LoadScene("MineMenu");
        }
        else
        {
            Debug.LogError("Next scene name is not set!");
        }
    }

    IEnumerator FadeImage(UnityEngine.UI.Image image, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = image.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, endAlpha);
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
