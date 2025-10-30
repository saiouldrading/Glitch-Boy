
// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class MainPlayer : MonoBehaviour
// {
//     public float speed = 3f;
//     public float jumpForce = 4f;
//     private float Walkspeed = 3f;
//     private float RunSpeed = 6f;

//     Rigidbody2D rb;
//     Animator animator;
//     AudioSource audioSource;

//     public Transform RespawnPoint;
//     public int maxHealth = 100;
//     public int currentHealth;
//     public HealBar healBar;

//     private bool isGrounded;
//     private bool canMove = true;
//     private bool isRunning = false;
//     private bool isJumping = false;
//     private Vector2 orignalscale;

//     public AudioClip jumpsound;
//     public AudioClip deadsound;
//     public float deaththreshold = -10f;
//     public GameObject Restartmenu;

//     // Double jump variables
//     private int jumpCount = 0;
//     public int maxJumps = 2;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//         audioSource = GetComponent<AudioSource>();
//         currentHealth = maxHealth;
//         healBar.SetMaxHealth(maxHealth);
//         orignalscale = transform.localScale;
//     }

//     void Update()
//     {
//         if (!canMove) return;

//         float inputX = Input.GetAxis("Horizontal");
//         rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);

//         Running(inputX);
//         Jumping();
//         flip(inputX);
//         animator.SetBool("IsRunning", Mathf.Abs(inputX) > 0.001f);

//         CheckDeathThreshold();
//     }

//     private void flip(float Xmovement)
//     {
//         if (Xmovement > 0.001f)
//             transform.eulerAngles = new Vector3(0f, 0f, 0f);
//         else if (Xmovement < -0.001f)
//             transform.eulerAngles = new Vector3(0f, 180f, 0f);
//     }

//     private void Jumping()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
//         {
//             rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reset vertical velocity
//             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//             jumpCount++;
//             isJumping = true;
//             audioSource.PlayOneShot(jumpsound);
//         }

//         animator.SetBool("IsJumping", isJumping);
//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = true;
//             isJumping = false;
//             jumpCount = 0; // Reset jump count
//         }
//     }

//     public void Takedamage(int damage)
//     {
//         currentHealth -= damage;
//         healBar.sethealth(currentHealth);

//         if (currentHealth <= 0)
//         {
//             Die();
//             canMove = false;
//             Restartmenu.SetActive(true);
//         }
//     }

//     public void Die()
//     {
//         animator.SetBool("IsDead", true);
//         audioSource.PlayOneShot(deadsound);
//     }

//     public void Respawn()
//     {
//         transform.position = RespawnPoint.position;
//         currentHealth = maxHealth;
//         healBar.sethealth(currentHealth);
//         canMove = true;
//         animator.SetBool("IsDead", false);
//         Restartmenu.SetActive(false);
//     }

//     public void Running(float Xmovement)
//     {
//         if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && Mathf.Abs(Xmovement) > 0.001f)
//             isRunning = true;

//         if (isRunning)
//         {
//             speed = RunSpeed;
//             animator.SetBool("Running", true);
//         }
//         else
//         {
//             speed = Walkspeed;
//             animator.SetBool("Running", false);
//         }

//         if (Input.GetKeyUp(KeyCode.LeftShift))
//             isRunning = false;
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Spikes"))
//         {
//             animator.SetBool("IsJumping", false);
//             Takedamage(100);
//         }
//     }

//     private void CheckDeathThreshold()
//     {
//         if (transform.position.y < deaththreshold && currentHealth > 0)
//         {
//             Takedamage(100);
//             Restartmenu.SetActive(true);
//         }
//     }
// }


using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPlayer : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 4f;
    private float Walkspeed = 3f;
    private float RunSpeed = 6f;

    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;

    public Transform RespawnPoint;
    public int maxHealth = 100;
    public int currentHealth;
    public HealBar healBar;

    private bool canMove = true;
    private bool isRunning = false;
    private bool isJumping = false;
    private Vector2 orignalscale;

    public AudioClip jumpsound;
    public AudioClip deadsound;
    public float deaththreshold = -10f;
    public GameObject Restartmenu;

    // Double jump variables
    private int jumpCount = 0;
    public int maxJumps = 2;

    // Raycast ground check
    [Header("Ground Check Settings")]
    public Transform groundCheck;   // Empty object placed at player’s feet
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healBar.SetMaxHealth(maxHealth);
        orignalscale = transform.localScale;
    }

    void Update()
    {
        if (!canMove) return;

        float inputX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);

        Running(inputX);
        Jumping();
        flip(inputX);
        animator.SetBool("IsRunning", Mathf.Abs(inputX) > 0.001f);

        CheckDeathThreshold();
    }

    private void flip(float Xmovement)
    {
        if (Xmovement > 0.001f)
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (Xmovement < -0.001f)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    private void Jumping()
    {
        bool isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < maxJumps))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            isJumping = true;
            audioSource.PlayOneShot(jumpsound);
        }

        // Reset jump count when grounded
        if (isGrounded)
        {
            jumpCount = 0;
            isJumping = false;
        }

        animator.SetBool("IsJumping", isJumping);
    }

    public void Takedamage(int damage)
    {
        currentHealth -= damage;
        healBar.sethealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            canMove = false;
            Restartmenu.SetActive(true);
        }
    }

    public void Die()
    {
        animator.SetBool("IsDead", true);
        audioSource.PlayOneShot(deadsound);
    }

    public void Respawn()
    {
        transform.position = RespawnPoint.position;
        currentHealth = maxHealth;
        healBar.sethealth(currentHealth);
        canMove = true;
        animator.SetBool("IsDead", false);
        Restartmenu.SetActive(false);
    }

    public void Running(float Xmovement)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(Xmovement) > 0.001f)
            isRunning = true;

        if (isRunning)
        {
            speed = RunSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            speed = Walkspeed;
            animator.SetBool("Running", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            animator.SetBool("IsJumping", false);
            Takedamage(100);
        }
    }

    private void CheckDeathThreshold()
    {
        if (transform.position.y < deaththreshold && currentHealth > 0)
        {
            Takedamage(100);
            Restartmenu.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}
