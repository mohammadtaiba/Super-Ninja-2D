using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Flag : MonoBehaviour
{
    public bool canMoveToNextScene = false;
    public static int totalEnemies = 0;
    public TMP_Text EnemiesCount;
    public bool IsEndLevel = false;
    public Animator Anim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && canMoveToNextScene)
        {
            if(!IsEndLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Anim.SetBool("open", true);
            }
        }
    }


   
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;
        EnemiesCount.text = "Enemies : " + totalEnemies;
        if (totalEnemies<=0)
        {
            //if(IsEndLevel)
            //{
            //    Anim.SetBool("open", true);

            //}

            canMoveToNextScene = true;
        }

      
    }

    public void GameWonScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
