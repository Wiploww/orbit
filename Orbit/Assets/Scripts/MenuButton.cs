using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] Material white;
    [SerializeField] Material white_emissive;
    [SerializeField] AudioClip ting;

    enum ButtonAction
    {
        None, Play, LevelSelect, Options, Quit
    };
    
    [SerializeField] ButtonAction buttonAction;
    
    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material = white_emissive;
        AudioSource.PlayClipAtPoint(ting, Camera.main.transform.position, 4);
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material = white;
    }

    private void OnMouseUp()
    {
        switch(buttonAction)
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
