using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] bool isMoveable;
    [SerializeField] Material outline;
    //[SerializeField] GameObject spaceSlider;
    
    Vector3 mousePositionWorld;
    Camera cam;
    float cubeZ;
    int layerMask = 6;

    private void Start()
    {
        cam = Camera.main;
        
        if (SceneManager.GetActiveScene().name == "MainMenu") { cubeZ = 41.29f; }
        else { cubeZ = 48.8f; }

        if (isMoveable) 
        {
            Material[] mats = GetComponent<Renderer>().materials;
            mats[1] = outline;
            GetComponent<Renderer>().materials = mats;
        }


        //if (isMoveable) { spaceSlider.SetActive(true); }
    }

    private void OnMouseDrag()
    {
        if (isMoveable)
        {
            Debug.Log(mousePositionWorld);
            mousePositionWorld = Input.mousePosition;
            
            mousePositionWorld.z = cubeZ;
            mousePositionWorld = cam.ScreenToWorldPoint(mousePositionWorld);

            float distanceFromCenter = Mathf.Sqrt((Mathf.Pow(mousePositionWorld.x, 2) - 0) + (Mathf.Pow(mousePositionWorld.z, 2) - 0));
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //Physics.Raycast(ray, out hit, 100, layerMask);

            if (distanceFromCenter < 18.5f)
            {
                gameObject.transform.position = mousePositionWorld;
            }
            else
            {
                Debug.Log("Too far :[");
            }
        }
    }
}
