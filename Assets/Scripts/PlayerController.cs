using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Public variables
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private int jumpsLeft;
    public int maxJumps = 2;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 1;

    // Private variables
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;
    //private Health health;
   // private Inventory inventory;
    public InputManager inputManager;
    private bool isGrounded;



    /// <summary>
    // audios files for sound
    public AudioClip pickupSound;
    public AudioClip jumpSound;
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
      //  health = GetComponent<Health>();
      //  inventory = GetComponent<Inventory>();
       // inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<= -15.16f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // Handle movement input
        float horizontalMove = inputManager.GetHorizontalMove();
        rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);

        // Flip the sprite based on direction
        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        // Handle jumping input
        if (inputManager.GetJumpInput())
        {
            if (jumpsLeft > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsLeft--;
               
                // audioSource.PlayOneShot(jumpSound);
            }
        }

        // Handle attacking input
        if (inputManager.GetAttackInput())
        {
            Attack();
        }
        float newY = Mathf.Clamp(transform.position.y, -10f, 4.6f);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if(isGrounded)
        {
            jumpsLeft = maxJumps;
            anim.SetBool("IsJumping", false);
        }
        else
        {
            anim.SetBool("IsJumping", true);

        }
        // Set the animation parameter for speed

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

   

  

    private void Attack()
    {
        // Play attack animation
        anim.SetTrigger("Attack");
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySwordSound();

        }
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage enemies in range
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw attack range in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
