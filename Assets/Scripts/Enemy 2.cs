using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private LayerMask playerLayer;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 1.5f;
    
    [Header("Combat Settings")]
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float damageInterval = 1f; // Damage kitni der baad dega
    
    [Header("References")]
    public Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    
    private SpriteRenderer spriteRenderer;
    private bool playerDetected = false;
    private Vector2 movement;
    private float lastDamageTime = 0f;
    
    void Start()
    {
        // Agar rb manually assign nahi kiya to get karo
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Agar animator manually assign nahi kiya to get karo
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        // Player ko find karo agar assign nahi hai
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
        
        // Rigidbody2D settings - GRAVITY DISABLE
        if (rb != null)
        {
            rb.gravityScale = 0; // Gravity band
            rb.freezeRotation = true; // Rotation lock karo
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Better collision
            rb.interpolation = RigidbodyInterpolation2D.Interpolate; // Smooth movement
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; // Y-position aur rotation freeze
        }
    }
    
    void Update()
    {
        if (player == null) return;
        
        // Check karo ke player alive hai ya nahi
        MainPlayer playerScript = player.GetComponent<MainPlayer>();
        if (playerScript != null && playerScript.currentHealth <= 0)
        {
            // Agar player dead hai to movement band karo
            movement = Vector2.zero;
            UpdateAnimation(false);
            return;
        }
        
        // Player ko detect karo
        DetectPlayer();
        
        if (playerDetected)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            // Agar stopping distance se zyada door hai to move karo
            if (distanceToPlayer > stoppingDistance)
            {
                // Sirf horizontal direction calculate karo (X-axis par)
                float directionX = Mathf.Sign(player.position.x - transform.position.x);
                movement = new Vector2(directionX, 0f);
                
                // Animation parameters set karo
                UpdateAnimation(true);
            }
            else
            {
                // Ruk jao
                movement = Vector2.zero;
                UpdateAnimation(false);
            }
            
            // Sprite ko flip karo player ki taraf
            FlipSprite();
        }
        else
        {
            movement = Vector2.zero;
            UpdateAnimation(false);
        }
    }
    
    void FixedUpdate()
    {
        // Movement apply karo
        if (rb != null)
        {
            rb.linearVelocity = movement * moveSpeed;
        }
        else
        {
            transform.position += (Vector3)movement * moveSpeed * Time.fixedDeltaTime;
        }
    }
    
    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        // Range check
        if (distanceToPlayer <= detectionRange)
        {
            // Raycast se check karo ke beech mein wall ya obstacle to nahi
            Vector2 direction = player.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, detectionRange);
            
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    playerDetected = true;
                    return;
                }
            }
        }
        
        playerDetected = false;
    }
    
    void UpdateAnimation(bool isMoving)
    {
        if (animator == null) return;
        
        // Animator parameters set karo
        animator.SetBool("WALKING", isMoving);
        // animator.SetFloat("Speed", isMoving ? moveSpeed : 0f);
        
        // Horizontal movement ke liye (vertical nahi chahiye)
        if (isMoving)
        {
            animator.SetFloat("Horizontal", movement.x);
        }
    }
    
    void FlipSprite()
    {
        if (spriteRenderer == null) return;
        
        // Player right side par hai to sprite flip karo
        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    
    // Debug visualization
    void OnDrawGizmosSelected()
    {
        // Detection range circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // Stopping distance circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
        
        // Line to player agar detected hai
        if (playerDetected && player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
    
    // Player se collision/trigger par damage do
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer(collision.gameObject);
        }
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Agar damage interval guzar gaya hai to damage do
            if (Time.time >= lastDamageTime + damageInterval)
            {
                DamagePlayer(collision.gameObject);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DamagePlayer(other.gameObject);
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Agar damage interval guzar gaya hai to damage do
            if (Time.time >= lastDamageTime + damageInterval)
            {
                DamagePlayer(other.gameObject);
            }
        }
    }
    
    void DamagePlayer(GameObject playerObject)
    {
        MainPlayer playerScript = playerObject.GetComponent<MainPlayer>();
        
        if (playerScript != null)
        {
            playerScript.Takedamage(damageAmount);
            lastDamageTime = Time.time;
        }
    }
}