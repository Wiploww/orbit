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

            if (screen.alpha < 1)
            {
                screen.alpha += .01f + Time.deltaTime;
            }

            //Debug.Log(PlayerPrefs.GetInt("CurrentLevel") + ", " + PlayerPrefs.GetInt("LevelMax"));

            if (PlayerPrefs.GetInt("LevelMax") <= PlayerPrefs.GetInt("CurrentLevel") && win == false)
            {
                PlayerPrefs.SetInt("LevelMax", PlayerPrefs.GetInt("CurrentLevel") + 1);
            }

            //Debug.Log(PlayerPrefs.GetInt("CurrentLevel") + ", " + PlayerPrefs.GetInt("LevelMax"));


            win = true;
        }
    }

    void WinCheck()
    {
        foreach(LightCondition c in cubes)
        {
            if(c.conditionMet == false)
            {
                if(winVolume.weight > 0)
                {
                    winVolume.weight -= .03f + Time.deltaTime;
                }
                else
                {
                    winVolume.weight = 0;
                }
            }
        }

        winVolume.weight += Time.deltaTime;
    }

    void StarCount()
    {
        if(starTimer > 0)
        {
            starTimer -= Time.deltaTime;
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
