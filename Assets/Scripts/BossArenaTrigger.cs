using UnityEngine;

public class BossArenaTrigger : MonoBehaviour
{
    [Header("Arena Setup")]
    [SerializeField] private GameObject[] arenaWalls;
    [SerializeField] private GameObject bossGameObject;
    
    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip bossMusicClip;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            ActivateArena();
        }
    }

    private void ActivateArena()
    {
        hasTriggered = true;

        // Lock the player inside by enabling wall colliders
        foreach (GameObject wall in arenaWalls)
        {
            if (wall != null)
            {
                BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();
                if (wallCollider != null)
                {
                    wallCollider.enabled = true;
                }
                else
                {
                    // If no BoxCollider2D, just activate the GameObject as a fallback
                    wall.SetActive(true);
                }
            }
        }

        // Activate the Boss
        if (bossGameObject != null)
        {
            bossGameObject.SetActive(true);
        }

        // Play dramatic music
        if (musicSource != null && bossMusicClip != null)
        {
            musicSource.clip = bossMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        }

        Debug.Log("Boss Arena Activated!");
    }
}
