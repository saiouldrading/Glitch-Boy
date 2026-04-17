using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteCanvas;
    [SerializeField] private float delayBeforeNextLevel = 3.0f;

    public void BossDefeated()
    {
        if (levelCompleteCanvas != null)
        {
            levelCompleteCanvas.SetActive(true);
        }

        StartCoroutine(LoadNextLevelAfterDelay());
    }

    private IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeNextLevel);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Optionally, check if we're at the last level or need a specific range
        // For Level 1 to 5, we can use a simpler approach or a dedicated level index
        SceneManager.LoadScene(nextSceneIndex);
    }
}
