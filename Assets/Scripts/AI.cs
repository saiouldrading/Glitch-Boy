using UnityEngine;
using UnityEngine.AI;

public class StationaryEnemyWithAnimation : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 15f;
    public float fieldOfView = 90f;
    
    private NavMeshAgent agent;
    private Animator animator;
    private bool hasSeenPlayer = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (CanSeePlayer())
        {
            // if (!hasSeenPlayer)
            // {
            //     // پہلی بار player دیکھا تو animation start کریں
            //     OnPlayerSpotted();
            // }
            // hasSeenPlayer = true;
            ChasePlayer();
        }
        else if (hasSeenPlayer)
        {
            ChasePlayer();
        }

        // Animation parameters update کریں
        UpdateAnimations();
    }

    bool CanSeePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
            return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        
        if (angle > fieldOfView / 2)
            return false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    // void OnPlayerSpotted()
    // {
    //     // جب player پہلی بار دیکھے تو animation play کریں
    //     if (animator != null)
    //     {
    //         animator.SetTrigger("PlayerSpotted");
    //     }
    // }

    void UpdateAnimations()
    {
        if (animator == null) return;

        // Speed based animation
        float speed = agent.velocity.magnitude;
        // animator.SetFloat("Speed", speed);

        // Chase state
        animator.SetBool("isWalking", hasSeenPlayer);

        // Attack animation (اگر player بہت قریب ہو)
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // if (distanceToPlayer <= agent.stoppingDistance + 0.5f)
        // {
        //     animator.SetTrigger("Attack");
        // }
    }

    public void ResetEnemy()
    {
        hasSeenPlayer = false;
        agent.isStopped = true;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }

    // Animation Events کے لیے functions
    public void OnAttackStart()
    {
        // Attack شروع ہونے پر کچھ خاص logic
        Debug.Log("Attack Started!");
    }

    public void OnAttackEnd()
    {
        // Attack ختم ہونے پر logic
        Debug.Log("Attack Ended!");
    }
}