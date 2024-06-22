using System;
using System.Collections;
using TMPro;
using TouhouPride;
using TouhouPride.Love;
using TouhouPride.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public TMP_Text ReadyText;
    
    public static StageManager instance { get; private set; }

    public GameObject GameOverScreen;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // disable game over screen in case its enabled
        GameOverScreen.SetActive(false); // = false;
        // play stage music
        AudioManager.instance.PlayMusic(FModReferences.instance.stage);
        
        // disable ready text
        StartCoroutine(Ready());
        
    }

    public void RestartGame()
    {
        Unpause();
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene("Menu");
    }

    public void ShowGameOver()
    {
        AudioManager.instance.PauseMusic();
        AudioManager.instance.PlayOneShot(FModReferences.instance.defeat, PlayerManager.Instance.Player.transform.position);
        GameOverScreen.SetActive(true);
        Pause();
    }

    public void Continue()
    {
        // give the player some extra love points so they don't die immediately 
        for (int i = 0; i < 3; i++)
        {
            LoveMeter.Instance.AddPoint(StaticData.CharacterName);
        }
        
        AudioManager.instance.UnPauseMusic();
        GameOverScreen.SetActive(false);
        Unpause();
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
    }

    IEnumerator Ready()
    {
        yield return new WaitForSeconds(6f);
        ReadyText.enabled = false;
    }
}
