using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject[] levels;

    private void Awake()
    {
        Instantiate(levels[PlayerPrefs.GetInt("CurrentLevel")]);
    }
}