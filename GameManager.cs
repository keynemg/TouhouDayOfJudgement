using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public GameObject pauseMenu;
    public GameObject VictoryScreen;
    public GameObject DefeatScreen;
    public float defeatalpha = 0f;
    public float victoryalpha = 0f;
    public bool defeated = false;
    public bool victory = false;

    public AudioClip btn_SFX;

    void Awake()
    {
        instance = this;
        ResumeGame();
    }
    void Update()
    {
        if (defeated)
        {
            AudioManager.instance.MusicFade = true;
            defeatalpha += 0.02f;
            DefeatScreen.GetComponent<CanvasGroup>().alpha = defeatalpha;
        }

        if (victory)
        {
            AudioManager.instance.MusicFade = true;
            victoryalpha += 0.015f;
            VictoryScreen.GetComponent<CanvasGroup>().alpha = victoryalpha;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayBtnSFX();
                if (pauseMenu.activeInHierarchy)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
        }
    }

    public void PlayBtnSFX()
    {
        AudioManager.instance.PlaySingle(btn_SFX);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        HideHelp();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ShowHelp()
    {
        PlayBtnSFX();
        pauseMenu.transform.GetChild(5).gameObject.SetActive(true);
    }
    public void HideHelp()
    {
        if (pauseMenu.transform.GetChild(5).gameObject.activeInHierarchy)
        {
            PlayBtnSFX();
        }
        pauseMenu.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
        pauseMenu.transform.GetChild(5).GetChild(1).gameObject.SetActive(false);
        pauseMenu.transform.GetChild(5).gameObject.SetActive(false);
    }
    public void GoToMenu()
    {
        PlayBtnSFX();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VictoryScreen.SetActive(true);
            victory = true;
            Player_Stats.Instance.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
