using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool isdead;
    public float attackRange = 1f;
    public float chaseRange = 5f;
    public float moveSpeed = 2f;
     public float leftBoundary = -2f;
     public float rightBoundary = 2f;
  
    public int maxHealth = 3;
    public int damage = 1;
    public float detectionRadius = 2f;

    public Animator animator;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private int currentHealth;
    private bool isAttacking = false;
   // private bool isHurt = false;
    private Transform player;



    public float sightRange = 5f;
    public LayerMask playerLayer;

    public Vector2 direction;

    public GameObject Particles;

    public bool IsDragon = false;
    public GameObject Potion;
    public float LocalScale = 1f;
    void Start()
    {

        rightBoundary = transform.position.x + 2;
        leftBoundary = transform.position.x - 2;

    
        
           isdead = false;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(isdead)
        {
            return;
        }
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        else if (distance <= chaseRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (movingRight && !isAttacking)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(LocalScale, LocalScale, LocalScale);
            direction= new Vector3(1f, 0);
        }
        else if (!isAttacking)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-LocalScale, LocalScale, LocalScale);
            direction = new Vector3(-1f, 0);

        }

        // Check if the enemy has hit a boundary and should change direction
        if (transform.position.x >= rightBoundary)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftBoundary)
        {
            movingRight = true;
        }
    }

    void Chase()
    {
     //   animator.SetBool("IsMoving", true);
        animator.SetBool("Attack", false);

        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(LocalScale, LocalScale);
            direction = new Vector3(1f, 0);

        }
        else
        {
            transform.localScale = new Vector2(-LocalScale, LocalScale);
            direction = new Vector3(-LocalScale, 0);

        }

        float horizontalInput = transform.localScale.x;
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayEnemydeathSound();

            }
            isdead = true;
            animator.SetTrigger("death");
            if(!IsDragon)
            {
                Instantiate(Potion, this.transform.position, Quaternion.identity);
            }
          
        }
        //else
        //{
        //    StartCoroutine(Hurt());
        //}

    }

    private void Die()
    {
         GameObject par= Instantiate(Particles, transform.position, Quaternion.identity) as GameObject;
        player.GetComponent<CoinPicker>().coins += 10;
        player.GetComponent<CoinPicker>().UpdateCoinText();

        par.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
        Destroy(par, 1.3f);
    }


    public void Detectattack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, sightRange, playerLayer);
        Debug.DrawRay(transform.position, direction, Color.red);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(10);
           
           
        }
    }
    

    private IEnumerator Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");
        isAttacking = true;

        // Wait for attack animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // End attack animation and reset attack flag
        animator.ResetTrigger("Attack");
        isAttacking = false;
    }

    //private IEnumerator Hurt()
    //{
    //    // Play hurt animation
    //    animator.SetTrigger("Hurt");
    //    isHurt = true;

    //    // Wait for hurt animation to finish
    //    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

    //    // End hurt animation and reset hurt flag
    //    animator.ResetTrigger("Hurt");
    //    isHurt = false;
    //}
}
