using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public AudioSource CoinsAdsource, GameOverAdsource, EnemydeathAdsource, AttackAdsource, BgAdSource;

    public AudioClip coinclip, gameoverclip, enemydeathclip, attackclip, bgclip;

    public static SoundManager Instance;



    public Slider soundslider, musicslider;
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
    private void Start()
    {
        musicslider.value = PlayerPrefs.GetFloat("m", 1);
        soundslider.value = PlayerPrefs.GetFloat("s", 1);
        CoinsAdsource.volume = soundslider.value;
        GameOverAdsource.volume = soundslider.value;
        EnemydeathAdsource.volume = soundslider.value;
        AttackAdsource.volume = soundslider.value;
        BgAdSource.volume = musicslider.value;
    }
    public void PlayCoinSound()
    {
        CoinsAdsource.clip = coinclip;
        CoinsAdsource.Play();
    }

    public void PlayEnemydeathSound()
    {
        EnemydeathAdsource.clip = enemydeathclip;
        EnemydeathAdsource.Play();
    }

    public void PlaySwordSound()
    {
        AttackAdsource.clip = attackclip;
        AttackAdsource.Play();
    }

    public void PlayGameoverSound()
    {
        GameOverAdsource.clip = gameoverclip;
        GameOverAdsource.Play();
    }

    public void changeSound(float val)
    {
        soundslider.value = val;
        PlayerPrefs.SetFloat("s", soundslider.value);
        CoinsAdsource.volume = soundslider.value;
        GameOverAdsource.volume = soundslider.value;
        EnemydeathAdsource.volume = soundslider.value;
        AttackAdsource.volume = soundslider.value;
       
    }

    public void changeMusic(float val)
    {
        musicslider.value = val;
        PlayerPrefs.SetFloat("m", musicslider.value);
        BgAdSource.volume = musicslider.value;

    }
}
