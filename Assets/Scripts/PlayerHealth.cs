using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;

    public Slider HealthBar;



    private void Start()
    {
        HealthBar.maxValue = maxHealth;
        HealthBar.value = maxHealth;
        currentHealth = maxHealth;
    }

    public void Revive()
    {
        HealthBar.maxValue = maxHealth;
        HealthBar.value = maxHealth;
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Potion"))
        {
            Destroy(collision.gameObject);
            currentHealth += 15;
            HealthBar.value = currentHealth;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);
        HealthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayGameoverSound();

            }
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        animator.SetTrigger("Die");
        // TODO: Implement death behavior, such as respawning or game over screen
        GameManager.Instance.GameOver();
       
        //Destroy(this.gameObject);
    }


   
}
