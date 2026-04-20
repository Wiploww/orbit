using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] const int LEVEL_COUNT = 9;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Volume pauseVolume;
    [SerializeField] GameObject optionScreen;
    [SerializeField] GameObject playerPrefsCheck;
    [SerializeField] GameObject colorTips;

    enum Scenes
    {
        MainMenu,
        LevelSelect,
        Gameplay,
        Null
    };

    Scenes currentScene;
    bool tips;
    public bool settings;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("LevelMax").Equals(null))
        {
            PlayerPrefs.SetInt("LevelMax", 0);
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }

        currentScene = (Scenes)SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        Time.timeScale = 1;

        if (currentScene == Scenes.LevelSelect)
        {
            for (int i = 1; i <= 15; i++)
            {
                GameObject button = GameObject.Find("Level " + (i + 1));

                if (PlayerPrefs.GetInt("LevelMax") >= i)
                {
                    button.transform.GetChild(1).gameObject.SetActive(false);
                    button.GetComponent<Animator>().SetBool("locked", false);
                    button.GetComponent<Animator>().ResetTrigger("click");
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void Update()
    {
        //esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentScene)
            {
                case Scenes.MainMenu:
                    Settings(false);
                    break;
                case Scenes.LevelSelect:
                    MainMenu();
                    break;
                default:
                    Pause();
                    break;
                
            }
        }

        //R
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(currentScene == Scenes.Gameplay)
            {
                Play();
            }
        }
    }

    public void Pause()
    {
        if (pauseVolume.weight == 1f && !tips)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            pauseVolume.weight = 0f;
        }
        else if (!tips)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            pauseVolume.weight = 1f;
        }
        else if (tips)
        {
            Tips();
        }
    }
    public void Settings(bool doOpen)
    {
        optionScreen.SetActive(doOpen);
        settings = doOpen;

        if (doOpen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void CheckPlayerPrefs(bool doOpen)
    {
        playerPrefsCheck.SetActive(doOpen);
    }
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Tips()
    {
        if (tips)
        {
            colorTips.SetActive(false);
            tips = false;
        }
        else
        {
            colorTips.SetActive(true);
            tips = true;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void SelectLevel()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        button.GetComponent<Animator>().SetTrigger("click");

        for (int i = 0; i <= 15; i++)
        {
            if(button.name == "Level " + i)
            {
                if(PlayerPrefs.GetInt("LevelMax") >= i - 1)
                {
                    PlayerPrefs.SetInt("CurrentLevel", i - 1);
                    Play();
                }
                else
                {
                    Debug.Log("Locked");
                }
            }
        }
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        if(PlayerPrefs.GetInt("CurrentLevel") == LEVEL_COUNT - 1)
        {
            LevelSelect();
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
            Play();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
