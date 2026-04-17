using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameVictoryManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject victoryCanvas;
    
    [Header("FX References")]
    [SerializeField] private GameObject confettiPrefab;
    [SerializeField] private int confettiCount = 5;
    [SerializeField] private float spawnRadius = 3f;

    [Header("Settings")]
    [SerializeField] private float slowMotionScale = 0.5f;
    [SerializeField] private float finalPauseDelay = 2.0f;

    private bool hasWon = false;

    public void OnFinalBossDefeated()
    {
        if (hasWon) return;
        hasWon = true;

        StartCoroutine(VictorySequence());
    }

    private IEnumerator VictorySequence()
    {
        // 1. Slow down time
        Time.timeScale = slowMotionScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Keep physics stable

        // 2. Spawn Confetti around the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && confettiPrefab != null)
        {
            for (int i = 0; i < confettiCount; i++)
            {
                Vector3 spawnPos = player.transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
                Instantiate(confettiPrefab, spawnPos, Quaternion.identity);
            }
        }

        // 3. Wait for 2 seconds (unscaled time because timeScale is 0.5)
        yield return new WaitForSecondsRealtime(finalPauseDelay);

        // 4. Pause the game and show UI
        Time.timeScale = 0f;
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
        }
    }

    // Button Functions
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MineMenu"); // Based on your file structure
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
