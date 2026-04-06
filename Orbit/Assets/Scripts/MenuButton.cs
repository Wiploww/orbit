using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] Material mainColor;
    [SerializeField] Material colorEmissive;
    bool buttonClicked = false;

    enum ButtonAction
    {
        None, Play, LevelSelect, Options, Quit
    };
    
    [SerializeField] ButtonAction buttonAction;
    
    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material = colorEmissive;
        buttonClicked = true;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material = mainColor;
        buttonClicked = false;
    }

    private void OnMouseUp()
    {
        if (buttonClicked)
        {
            switch (buttonAction)
            {
                case ButtonAction.Play:
                    buttonManager.Play();
                    break;

                case ButtonAction.LevelSelect:
                    buttonManager.LevelSelect();
                    break;

                case ButtonAction.Options:
                    buttonManager.OpenSettings();
                    break;

                case ButtonAction.Quit:
                    buttonManager.Quit();
                    break;
            }
        }
    }
}
