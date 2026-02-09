using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionScreen;
    [SerializeField] GameObject colorTips;

    bool paused;
    bool tips;
    bool settings;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("LevelMax").Equals(null))
        {
            PlayerPrefs.SetInt("LevelMax", 0);
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
    }

    private void Start()
    {
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "LevelSelect")
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (paused && !tips)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            paused = false;
        }
        else if (!tips)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            paused = true;
        }
        else if (tips)
        {
            Tips();
        }
    }
    public void OpenSettings()
    {
        optionScreen.SetActive(true);
        settings = true;
    }
    public void CloseSettings()
    {
        optionScreen.SetActive(false);
        settings = false;
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
        if(PlayerPrefs.GetInt("CurrentLevel") == 4)
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
