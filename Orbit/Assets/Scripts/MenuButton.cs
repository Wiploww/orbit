using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : Clickable
{
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] Material mainColor;
    [SerializeField] Material colorEmissive;
    bool buttonClicked = false;
    [SerializeField] float dragTime = 0;

    enum ButtonAction
    {
        None, Play, LevelSelect, Options, Quit
    };
    
    [SerializeField] ButtonAction buttonAction;

    private void Start()
    {
        BaseStart();
    }

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

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            dragTime = 0;
        }
    }

    private void OnMouseDrag()
    {
        dragTime += Time.deltaTime;
    }

    private void OnMouseUp()
    {
        if (buttonClicked && dragTime < .3f)
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
                    buttonManager.Settings(true);
                    break;

                case ButtonAction.Quit:
                    buttonManager.Quit();
                    break;
            }
        }
    }
}
