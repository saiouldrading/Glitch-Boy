using UnityEngine;

public class SmartEnemy2DAI : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float losePlayerRange = 12f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float chaseSpeed = 5.5f;
    [SerializeField] private float stoppingDistance = 1.5f;
    [SerializeField] private float patrolSpeed = 2f;
    
    [Header("Edge Detection")]
    [SerializeField] private float edgeCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 edgeCheckOffset = new Vector2(0.5f, -0.3f);
    
    [Header("Patrol Settings")]
    [SerializeField] private bool shouldPatrol = true;
    [SerializeField] private float patrolWaitTime = 2f;
    [SerializeField] private float minPatrolDistance = 3f;
    [SerializeField] private float maxPatrolDistance = 7f;
    
    [Header("Combat Settings")]
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private float attackRange = 1.2f;
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    // AI States
    private enum AIState { Idle, Patrol, Chase, Attack, ReturnToPatrol }
    private AIState currentState = AIState.Idle;
    
    // Movement
    private Vector2 movement;
    private int facingDirection = 1; // 1 = right, -1 = left
    private float currentSpeed;
    
    // Combat
    private float lastDamageTime;
    private bool canSeePlayer;
    
    // Patrol
    private Vector2 patrolStartPoint;
    private Vector2 patrolTargetPoint;
    private float patrolWaitTimer;
    private bool isWaitingAtPatrolPoint;
    
    // Edge Detection
    private bool isNearEdge;
    
    void Start()
    {
        InitializeComponents();
        SetupRigidbody();
        
        patrolStartPoint = transform.position;
        currentSpeed = patrolSpeed;
        
        if (shouldPatrol)
        {
            SetNewPatrolTarget();
            currentState = AIState.Patrol;
        }
    }
    
    void InitializeComponents()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
        
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }
    
    void SetupRigidbody()
    {
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    
    void Update()
    {
        if (player == null) return;
        
        // Check player alive status
        if (IsPlayerDead())
        {
            ChangeState(AIState.Idle);
            return;
        }
        
        // Edge detection
        CheckForEdge();
        
        // Player detection
        DetectPlayer();
        
        // State machine
        UpdateAIState();
    }
    
    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = movement * currentSpeed;
        }
        else
        {
            transform.position += (Vector3)movement * currentSpeed * Time.fixedDeltaTime;
        }
    }
    
    void UpdateAIState()
    {
        switch (currentState)
        {
            case AIState.Idle:
                HandleIdleState();
                break;
                
            case AIState.Patrol:
                HandlePatrolState();
                break;
                
            case AIState.Chase:
                HandleChaseState();
                break;
                
            case AIState.Attack:
                HandleAttackState();
                break;
                
            case AIState.ReturnToPatrol:
                HandleReturnToPatrolState();
                break;
        }
        
        UpdateAnimation();
    }
    
    void HandleIdleState()
    {
        movement = Vector2.zero;
        
        if (canSeePlayer && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            ChangeState(AIState.Chase);
        }
        else if (shouldPatrol)
        {
            ChangeState(AIState.Patrol);
        }
    }
    
    void HandlePatrolState()
    {
        if (canSeePlayer && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            ChangeState(AIState.Chase);
            return;
        }
        
        if (isWaitingAtPatrolPoint)
        {
            movement = Vector2.zero;
            patrolWaitTimer += Time.deltaTime;
            
            if (patrolWaitTimer >= patrolWaitTime)
            {
                isWaitingAtPatrolPoint = false;
                SetNewPatrolTarget();
            }
            return;
        }
        
        // Agar edge pe hai to direction change karo
        if (isNearEdge)
        {
            facingDirection *= -1;
            SetNewPatrolTarget();
            return;
        }
        
        float distanceToTarget = Vector2.Distance(transform.position, patrolTargetPoint);
        
        if (distanceToTarget < 0.3f)
        {
            // Patrol point reached
            isWaitingAtPatrolPoint = true;
            patrolWaitTimer = 0f;
            movement = Vector2.zero;
        }
        else
        {
            // Move towards patrol target
            float directionX = Mathf.Sign(patrolTargetPoint.x - transform.position.x);
            movement = new Vector2(directionX, 0f);
            UpdateFacingDirection(directionX);
        }
        
        currentSpeed = patrolSpeed;
    }
    
    void HandleChaseState()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        // Agar player bahut door ho gaya to chase chod do
        if (!canSeePlayer || distanceToPlayer > losePlayerRange)
        {
            ChangeState(AIState.ReturnToPatrol);
            return;
        }
        
        // Agar attack range me aa gaya to attack karo
        if (distanceToPlayer <= attackRange)
        {
            ChangeState(AIState.Attack);
            return;
        }
        
        // Agar edge pe hai to ruk jao
        if (isNearEdge && Mathf.Sign(player.position.x - transform.position.x) == facingDirection)
        {
            movement = Vector2.zero;
            return;
        }
        
        // Player ki taraf move karo
        if (distanceToPlayer > stoppingDistance)
        {
            float directionX = Mathf.Sign(player.position.x - transform.position.x);
            movement = new Vector2(directionX, 0f);
            UpdateFacingDirection(directionX);
        }
        else
        {
            movement = Vector2.zero;
        }
        
        currentSpeed = chaseSpeed;
    }
    
    void HandleAttackState()
    {
        movement = Vector2.zero;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        // Agar player door ho gaya to chase mode me jao
        if (distanceToPlayer > attackRange * 1.5f)
        {
            ChangeState(AIState.Chase);
            return;
        }
        
        // Player ko face karo
        float directionX = Mathf.Sign(player.position.x - transform.position.x);
        UpdateFacingDirection(directionX);
    }
    
    void HandleReturnToPatrolState()
    {
        if (canSeePlayer && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            ChangeState(AIState.Chase);
            return;
        }
        
        float distanceToStart = Vector2.Distance(transform.position, patrolStartPoint);
        
        if (distanceToStart < 0.5f)
        {
            ChangeState(AIState.Patrol);
            return;
        }
        
        // Start point ki taraf jao
        float directionX = Mathf.Sign(patrolStartPoint.x - transform.position.x);
        movement = new Vector2(directionX, 0f);
        UpdateFacingDirection(directionX);
        currentSpeed = moveSpeed;
    }
    
    void ChangeState(AIState newState)
    {
        currentState = newState;
    }
    
    void DetectPlayer()
    {
        if (player == null)
        {
            canSeePlayer = false;
            return;
        }
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = player.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, detectionRange, playerLayer | obstacleLayer);
            
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                canSeePlayer = true;
                return;
            }
        }
        
        canSeePlayer = false;
    }
    
    void CheckForEdge()
    {
        Vector2 checkPosition = (Vector2)transform.position + new Vector2(edgeCheckOffset.x * facingDirection, edgeCheckOffset.y);
        RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, edgeCheckDistance, groundLayer);
        
        isNearEdge = (hit.collider == null);
    }
    
    void SetNewPatrolTarget()
    {
        float patrolDistance = Random.Range(minPatrolDistance, maxPatrolDistance);
        patrolTargetPoint = patrolStartPoint + new Vector2(patrolDistance * facingDirection, 0);
    }
    
    void UpdateFacingDirection(float directionX)
    {
        if (directionX != 0)
        {
            facingDirection = (int)Mathf.Sign(directionX);
            
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = (facingDirection == 1);
            }
        }
    }
    
    void UpdateAnimation()
    {
        if (animator == null) return;
        
        bool isMoving = movement.magnitude > 0.1f;
        animator.SetBool("isWalking", isMoving);
        
        
        // if (isMoving)
        // {
        //     animator.SetFloat("Horizontal", movement.x);
            
        // }
    }
    
    bool IsPlayerDead()
    {
        if (player == null) return true;
        
        MainPlayer playerScript = player.GetComponent<MainPlayer>();
        return (playerScript != null && playerScript.currentHealth <= 0);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer(collision.gameObject);
        }
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= lastDamageTime + damageInterval)
        {
            DamagePlayer(collision.gameObject);
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
    
    void OnDrawGizmosSelected()
    {
        // Detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // Lose player range
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, losePlayerRange);
        
        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        // Edge detection
        Gizmos.color = isNearEdge ? Color.red : Color.green;
        Vector2 checkPos = (Vector2)transform.position + new Vector2(edgeCheckOffset.x * facingDirection, edgeCheckOffset.y);
        Gizmos.DrawLine(checkPos, checkPos + Vector2.down * edgeCheckDistance);
        
        // Patrol points
        if (shouldPatrol && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(patrolStartPoint, 0.3f);
            Gizmos.DrawWireSphere(patrolTargetPoint, 0.3f);
            Gizmos.DrawLine(patrolStartPoint, patrolTargetPoint);
        }
        
        // Line to player
        if (canSeePlayer && player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}