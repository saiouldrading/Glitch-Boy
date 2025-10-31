
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPlayer : MonoBehaviour
{
    public static MainPlayer Instance;
    public float speed = 3f;
    public float jumpForce = 4f;
    private float Walkspeed = 3f;
    private float RunSpeed = 6f;

    Rigidbody2D rb;
    Animator animator;
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceSFX;
    [Header("Health Settings")]
    public Transform RespawnPoint;
    public int maxHealth = 100;
    public int currentHealth;
    public HealBar healBar;
    [Header("Movement Control")]
    private bool canMove = true;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isDead = false;
    private Vector2 orignalscale;
    [Header("Audio Clips")]
    public AudioClip jumpsound;
    public AudioClip BGM;
    public AudioClip deadsound;
    public float deaththreshold = -10f;
    public GameObject Restartmenu;

    [Header("Jump Settings")]
    private int jumpCount = 0;
    public int maxJumps = 2;

    // Raycast ground check
    [Header("Ground Check Settings")]
    public Transform groundCheck;   // Empty object placed at player’s feet
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
    public LayerMask SecondaryGroundLayer;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healBar.SetMaxHealth(maxHealth);
        orignalscale = transform.localScale;
        audioSourceBGM.clip = BGM;
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
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
        bool isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer | SecondaryGroundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance, Color.red);

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < maxJumps))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            isJumping = true;
            animator.SetBool("IsJumping", true);
            audioSourceSFX.PlayOneShot(jumpsound);
        }

        if (isGrounded && rb.linearVelocity.y == 0)
        {
            jumpCount = 0;
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
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
        if (isDead) return;
        isDead = true;
        animator.SetBool("IsDead", true);
        audioSourceSFX.PlayOneShot(deadsound);
        audioSourceBGM.Pause();

    }

    public void Respawn()
    {
        transform.position = RespawnPoint.position;
        currentHealth = maxHealth;
        healBar.sethealth(currentHealth);
        canMove = true;
        animator.SetBool("IsDead", false);
        Restartmenu.SetActive(false);
        audioSourceBGM.Play();
        isDead = false;
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
