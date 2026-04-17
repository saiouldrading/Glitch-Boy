using UnityEngine;

public class BossDefeatedTrigger : MonoBehaviour
{
    private void OnDestroy()
    {
        // Example: Trigger when boss is destroyed
        if (!gameObject.scene.isLoaded) return; // Ignore when scene is unloading

        LevelManager levelManager = Object.FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.BossDefeated();
        }
    }

    // Manual trigger for testing
    [ContextMenu("Trigger Boss Defeated")]
    public void TriggerBossDefeated()
    {
        LevelManager levelManager = Object.FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.BossDefeated();
        }
    }
}
