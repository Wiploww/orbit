using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public bool win = false;
    [SerializeField] GameObject winScreen;
    [SerializeField] Volume winVolume;
    [SerializeField] Volume pauseVolume;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Slider starBar;
    [SerializeField] Image[] stars;
    [SerializeField] Image[] endStars;
    [SerializeField] Sprite starFull;
    [SerializeField] Sprite starEmpty;

    float starTimer = 90;

    CanvasGroup screen;
    LightCondition[] cubes;
    
    
    void Start()
    {
        cubes = FindObjectsByType<LightCondition>(FindObjectsSortMode.None);
        screen = winScreen.GetComponent<CanvasGroup>();

        Time.timeScale = 1;
    }

    void Update()
    {
        if (!win)
        {
            WinCheck();
            StarCount();
        }

        if (winVolume.weight >= 1) //Win!!!
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;

            if (screen.alpha < 1)
            {
                screen.alpha += .01f + Time.deltaTime;
                pauseVolume.weight += .01f + Time.deltaTime;
            }

            if (PlayerPrefs.GetInt("LevelMax") <= PlayerPrefs.GetInt("CurrentLevel") && win == false)
            {
                PlayerPrefs.SetInt("LevelMax", PlayerPrefs.GetInt("CurrentLevel") + 1);
            }

            win = true;
        }
    }

    void WinCheck()
    {
        foreach(LightCondition c in cubes)
        {
            if(c.conditionMet == false) //for each unsatisfied cube
            {
                if(winVolume.weight > 0) //decrease winVolume weight
                {
                    winVolume.weight -= .03f + Time.deltaTime;
                }
                else
                {
                    winVolume.weight = 0;
                }
            }
        }

        //every tick, increase winVolume weight
        winVolume.weight += Time.deltaTime;
    }

    void StarCount()
    {
        if(starTimer > 0)
        {
            starTimer -= Time.deltaTime * .5f;
            starBar.SetValueWithoutNotify(starTimer);
        }

        if(starTimer < 77)
        {
            stars[0].sprite = starEmpty;
            endStars[2].sprite = starEmpty;
        }
        
        if(starTimer < 55)
        {
            stars[1].sprite = starEmpty;
            endStars[1].sprite = starEmpty;
        }
        
        if (starTimer < 20)
        {
            stars[2].sprite = starEmpty;
            endStars[0].sprite = starEmpty;
        }
    }
}
