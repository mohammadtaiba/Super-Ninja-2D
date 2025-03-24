using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Mainmenupannel, SettingsPannel, GameOverPannel;

    public SpriteRenderer spriteRenderer;
    public PlayerController playerController;

    public PlayerHealth playerHealth;


    public static GameManager Instance;
    int count = 0;

    public float duration=.2f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }

    [System.Obsolete]
    private void Start()
    {
        playerController.enabled = false;
        Time.timeScale = 0f;
        Mainmenupannel.SetActive(true);
        
    }
    public void StartGame()
    {
        playerController.enabled = true;

        Time.timeScale = 1f;
        Mainmenupannel.SetActive(false);
       
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Mainmenupannel.SetActive(false);
        Time.timeScale = 1f;
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        GameOverPannel.SetActive(true);
        GameOverPannel.SetActive(true);

        spriteRenderer.enabled = false;
        playerController.enabled = false;
        Time.timeScale = 0f;
    }

   
  

    public void Revive()
    {
        GameOverPannel.SetActive(false);

        spriteRenderer.enabled = true;
        playerController.enabled = true;
        playerHealth.Revive();
        Time.timeScale = 1f;

    }

    public void SettingsFromGameOver()
    {
        count = 1;
        GameOverPannel.SetActive(false);
        SettingsPannel.SetActive(true);
       

    }

    public void SettingsFromMainMenu()
    {
        Mainmenupannel.SetActive(false);
        SettingsPannel.SetActive(true);
     


        count = 0;
    }

    public void SettingClose()
    {
        if (count == 1)
        {
            SettingsPannel.SetActive(false);
            GameOverPannel.SetActive(true);
           
        }
        else
        {
            SettingsPannel.SetActive(false);
            Mainmenupannel.SetActive(true);
            
        }
    }
}
