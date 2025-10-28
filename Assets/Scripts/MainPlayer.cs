// using UnityEngine;

// public class MainPlayer : MonoBehaviour

// {
//     public float speed = 5f;
//     public float jumpForce = 10f;
//     private Animator animator;
//     private Rigidbody2D rb;
//     private bool isGrounded = false;
//     private bool IsMoving = false;
//     private bool isJumping = false;

//     Vector2 orignalscale;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();


//         orignalscale = transform.localScale;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         float xmovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//         float ymovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
//         Vector2 move = new Vector2(xmovement, ymovement);
//         transform.Translate(move);

//         animator.SetBool("IsRunning", Mathf.Abs(xmovement) > 0.01f);
//         Flip(xmovement);
//         Jump();
//     }

//     void Flip(float xmovement)
//     {
//         Vector2 scale = orignalscale;
//         if (xmovement > 0.01f)
//         {
//             scale.x = Mathf.Abs(orignalscale.x);
//         }
//         else if (xmovement < -0.01f)
//         {
//             scale.x = -Mathf.Abs(orignalscale.x);
//         }
//         transform.localScale = scale;
//     }

//     private void Jump()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//         {
//             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//             isGrounded = false;
//             isJumping = true;
//         }
//         if (isJumping)
//         {
//             animator.SetBool("IsJumping", true);
//         }
//         else
//         {
//             animator.SetBool("IsJumping", false);
//         }
//     }
//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = true;
//               isJumping = false;
//         }
//     }
// }

// using UnityEngine;
// using UnityEngine.InputSystem.iOS;
// public class MainPlayer : MonoBehaviour
// {
//     public float speed = 5f;
//     private float Walkspeed = 2f;
// private float runspeed = 5f;
//     public float jumpForce = 6f;
//     private Rigidbody2D rb;
//     private bool isGrounded = false;
//     private bool isJumping = false;
//     private Animator animator;
//     private Vector2 orignalScale;
//     private bool isRunning = false;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//         orignalScale = transform.localScale;

//     }
//     void jump()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//         {
//             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//             isGrounded = false;
//             isJumping = true;
//         }


//         if (isJumping)
//         {
//             animator.SetBool("IsJumping", true);
//         }
//         else
//         {
//             animator.SetBool("IsJumping", false);
//         }

//     }
//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = true;
//             isJumping = false;
//         }
//     }
//     void Update()
//     {
//         float Xmovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//         float ymovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
//         Vector2 move = new Vector2(Xmovement, ymovement);
//         transform.Translate(move);
//         animator.SetBool("IsRunning", Mathf.Abs(Xmovement) > 0.001f);
//         jump();
//         flip(Xmovement);
//         Running(Xmovement);
//         Dead();

//     }


//     void flip(float Xmovement)
//     {
//         Vector2 scale = orignalScale;
//         if (Xmovement > 0.001f)
//         {
//             scale.x = Mathf.Abs(orignalScale.x);

//         }
//         else if (Xmovement < -0.001f)
//         {
//             scale.x = -Mathf.Abs(orignalScale.x);
//         }
//         transform.localScale = scale;

//     }

//     void Running(float Xmovement)
//     {
//         if (Input.GetKeyDown((KeyCode.LeftShift)))
//         {
//             isRunning = true;
//             speed = runspeed;
//         }

//         else if (Input.GetKeyUp((KeyCode.LeftShift)))
//         {
//             isRunning = false;
//             Xmovement = speed;
//             speed = Walkspeed;

//         }
//         if (isRunning)
//         {
//             animator.SetBool("Running", true);
//         }
//         else
//         {
//             animator.SetBool("Running", false);
//         }
//     }

//         void Dead()
//         {
//             if (Input.GetKeyDown(KeyCode.F))
//             {
//                 animator.SetBool("IsDead", true);
//             }

//         }

// }



// using UnityEngine;


// public class MainPlayer : MonoBehaviour
// {
//     public float speed = 2f;
//     public float runspeed = 5f;
//     private float Walkspeed = 2f;
//     public float jumpForce = 5f;
//     private bool isGrounded = false;
//     private bool isRunning = false;
//     private Rigidbody2D rb;
//     private Animator animator;
//     private bool isDead = false;
//     private bool isJumping = false;
//     private Vector2 orignalScale;
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//         orignalScale = transform.localScale;

//     }

//     void Jump()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//         {
//             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//             isJumping = true;
//             isGrounded = false;
//         }
//         if (isJumping)
//         {
//             animator.SetBool("IsJumping", true);

//         }
//         else
//         {
//             animator.SetBool("IsJumping", false);
//         }

//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = true;
//             isJumping = false;
//         }
//     }

//     void Update()
//     {
//         float Xmovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//         float Ymovement = 0f;
//         Vector2 move = new Vector2(Xmovement, Ymovement);
//         transform.Translate(move);
//         Jump();
//         flip(Xmovement);
//         animator.SetBool("IsRunning", Mathf.Abs(Xmovement) > 0.001f);
//         running(Xmovement);
//         Dead();
//     }

//     void flip(float Xmovement)
//     {
//         Vector2 scale = orignalScale;
//         if (Xmovement > 0.001)
//         {
//             scale.x = Mathf.Abs(orignalScale.x);
//         }
//         else if (Xmovement < -0.001f)
//         {
//             scale.x = -Mathf.Abs(orignalScale.x);
//         }
//         transform.localScale = scale;
//     }

//     void running(float Xmovement)
//     {
//         if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(Xmovement)>0.001f && isGrounded )
//         {
//             isRunning = true;
//         }
//         else if (Input.GetKeyUp(KeyCode.LeftShift))
//         {
//             isRunning = false;
//         }

//         if (isRunning)
//         {
//             animator.SetBool("Running", true);
//             speed = runspeed;

//         }
//         else
//         {
//             animator.SetBool("Running", false);
//             speed = Walkspeed;

//         }
//     }

//     void Dead()
//     {
//         if (Input.GetKeyDown(KeyCode.F))
//         {
//             isDead = true;
//         }
//         else if (Input.GetKeyUp(KeyCode.F))
//         {
//             isDead = false;
//         }

//         if (isDead)
//         {
//             animator.SetBool("IsDead", true);
//         }
//         else
//         {
//             animator.SetBool("IsDead", false);
//         }
//     }

// }



// using UnityEngine;
// using System.Collections;
// using Unity.Mathematics;

// public class MainPlayer : MonoBehaviour
// {
//     public float speed = 3f;
//     public float Walkspeed = 5f;
//     public float runspeed = 6f;
//     public float jumpForce = 10f;

//     private Rigidbody2D rb;
//     private Animator animator;

//     private bool isJumping = false;
//     private bool isGrounded = false;
//     private Vector2 orignalScale;

//     public Transform RespawnPoint;
//     public int maxHealth = 100;
//     public int currentHealth;
//     public HealBar healBar;


//     private bool canMove = true;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//         orignalScale = transform.localScale;
//         currentHealth = maxHealth;
//         healBar.SetMaxHealth(maxHealth);
//     }

//     private void jumping()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//         {
//             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//             isJumping = true;
//             isGrounded = false;
//         }

//         animator.SetBool("IsJumping", isJumping);
//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = true;
//             isJumping = false;
//         }
//     }

//     void flip(float Xmovement)
//     {
//         Vector2 scale = transform.localScale;
//         if (Xmovement > 0.001f)
//             scale.x = Mathf.Abs(orignalScale.x);
//         else if (Xmovement < -0.001f)
//             scale.x = -Mathf.Abs(orignalScale.x);

//         transform.localScale = scale;
//     }

//     void Update()
//     {
//         if (!canMove) return;

//         jumping();

//         float Xmovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//         Vector2 move = new Vector2(Xmovement, 0f);
//         transform.Translate(move);

//         animator.SetBool("IsRunning", Mathf.Abs(Xmovement) > 0.001f);
//         flip(Xmovement);
//         Running(Xmovement);
//     }

//     void Running(float Xmovement)
//     {
//         if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && Mathf.Abs(Xmovement) > 0.001f)
//         {
//             animator.SetBool("Running", true);
//             speed = runspeed;
//         }
//         else if (Input.GetKeyUp(KeyCode.LeftShift)&& -Mathf.Abs(Xmovement)<0.001f)
//         {
//             animator.SetBool("Running", false);
//             speed = Walkspeed;
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Spikes"))
//         {
//             animator.SetBool("IsJumping", false); 
//             TakeDamage(100);
//         }
//     }

//     void TakeDamage(int damage)
//     {
//         currentHealth -= damage;
//         healBar.sethealth(currentHealth);

//         if (currentHealth <= 0)
//         {
//             isDead(); 
//             canMove = false; 
//             StartCoroutine(RespawnWithDelay(2f)); 
//         }
//     }

//     IEnumerator RespawnWithDelay(float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         Respawn();
//     }

//     void Respawn()
//     {
//         transform.position = RespawnPoint.position;
//         currentHealth = maxHealth;
//         healBar.SetMaxHealth(maxHealth);
//         canMove = true;

//         animator.SetBool("IsDead", false); 
//     }

//     public void isDead()
//     {

//         animator.SetBool("IsDead", true);
//     }
// }



using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 4f;

    private float Walkspeed = 3f;
    private float RunSpeed = 6f;
    Rigidbody2D rb;
    Animator animator;
    public Transform RespawnPoint;
    public int maxHealth = 100;
    public int currentHealth;
    public HealBar healBar;

    private bool isGrounded;
    private bool canMove = true;
    private bool isRunning = false;
    private bool isJumping = false;
    private Vector2 orignalscale;

    public AudioClip jumpsound;
    private AudioSource audioSource;

    public AudioClip deadsound;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healBar.SetMaxHealth(maxHealth);
        orignalscale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    private void flip(float Xmovement)
    {
        if (Xmovement > 0.001f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Xmovement < -0.001f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            isGrounded = false;
            audioSource.PlayOneShot(jumpsound);
        }
        if (isJumping)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
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
            Restart.Instance.ShowRestartUI();
        }
    }

    IEnumerator RespawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Respawn();
    }

    public void Respawn()
    {
        transform.position = RespawnPoint.position;
        currentHealth = maxHealth;
        healBar.sethealth(currentHealth);
        canMove = true;
        animator.SetBool("IsDead", false);
        Restart.Instance.HideRestartUI();
    }

    public void Die()
    {
        animator.SetBool("IsDead", true);
        audioSource.PlayOneShot(deadsound);
    }

    public void Running(float Xmovement)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && Mathf.Abs(Xmovement) > 0.001f)
        {
            isRunning = true;
        }

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
        {
            isRunning = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            animator.SetBool("IsJumping", false);
            Takedamage(100);
        }
    }
        public void Update()
    {
        if (!canMove) return;

        float inputX = Input.GetAxis("Horizontal");
        // Move in world space using Rigidbody2D velocity for consistent movement
        rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);

        Running(inputX);
        Jumping();
        flip(inputX);
        animator.SetBool("IsRunning", Mathf.Abs(inputX) > 0.001f);
    }
}