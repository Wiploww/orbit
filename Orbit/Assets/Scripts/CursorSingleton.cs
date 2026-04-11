using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSingleton : MonoBehaviour
{
    public static CursorSingleton instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
