using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMovement : Clickable
{
    [SerializeField] Material outline;
    
    Vector3 mousePositionWorld;
    Camera cam;
    float cubeZ;

    //Ray ray;
    //RaycastHit[] hits;
    //int layerMask = 6;

    public void Start()
    {
        BaseStart();

        if (isInteractable) 
        {
            cam = Camera.main;

            if (SceneManager.GetActiveScene().name == "MainMenu") { cubeZ = 14.8f; }
            else { cubeZ = 48.8f; }

            Material[] mats = GetComponent<Renderer>().materials;
            mats[1] = outline;
            GetComponent<Renderer>().materials = mats;
        }
    }

    private void OnMouseDrag()
    {
        if (isInteractable)
        {
            mousePositionWorld = Input.mousePosition;
            
            mousePositionWorld.z = cubeZ;
            mousePositionWorld = cam.ScreenToWorldPoint(mousePositionWorld);

            float distanceFromCenter = Mathf.Sqrt((Mathf.Pow(mousePositionWorld.x, 2) - 0) + (Mathf.Pow(mousePositionWorld.z, 2) - 0));
            //ray = cam.ScreenPointToRay(Input.mousePosition);
            //Physics.RaycastAll(ray, out hits, 100, layerMask);

            if (distanceFromCenter < 18.5f)
            {
                transform.position = mousePositionWorld;
            }
            else
            {
                Debug.Log("Too far :[");
            }
        }
    }
}
