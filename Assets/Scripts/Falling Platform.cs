using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [System.Serializable]
    public class PlatformData
    {
        public GameObject platform;
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public bool isFalling = false;
        [HideInInspector] public Vector3 originalPosition;
    }

    public List<PlatformData> platforms = new List<PlatformData>();
    public float fallDelay = 2f;   // 2 sec baad girega
    public float resetDelay = 2f;  // 2 sec baad wapas aayega
    public float fallSpeed = 5f;

    void Start()
    {
        foreach (var p in platforms)
        {
            p.originalPosition = p.platform.transform.position;
            p.rb = p.platform.GetComponent<Rigidbody2D>();
            p.rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        foreach (var p in platforms)
        {
            // check if player touched this specific platform
            if (collision.collider.gameObject == p.platform || collision.otherCollider.gameObject == p.platform)
            {
                if (!p.isFalling)
                    StartCoroutine(FallAndReset(p));
            }
        }
    }

IEnumerator FallAndReset(PlatformData p)
{
    p.isFalling = true;

    yield return new WaitForSeconds(fallDelay);

    // Make platform fall
    p.rb.bodyType = RigidbodyType2D.Dynamic;
    p.rb.gravityScale = 2f; // or any value you like

    yield return new WaitForSeconds(resetDelay);

    // Reset
    p.rb.bodyType = RigidbodyType2D.Kinematic;
    p.rb.gravityScale = 0f;
    p.rb.linearVelocity = Vector2.zero;
    p.platform.transform.position = p.originalPosition;

    p.isFalling = false;
}

}
