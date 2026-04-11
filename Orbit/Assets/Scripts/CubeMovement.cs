using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] bool isMoveable;
    [SerializeField] Material outline;
    [SerializeField] CursorUI cursor;

    Vector3 mousePositionWorld;
    Camera cam;
    float cubeZ;
    int layerMask = 6;

    private void Start()
    {
        if (isMoveable) 
        {
            cam = Camera.main;

            if (SceneManager.GetActiveScene().name == "MainMenu") { cubeZ = 15.3f; }
            else { cubeZ = 48.8f; }

            Material[] mats = GetComponent<Renderer>().materials;
            mats[1] = outline;
            GetComponent<Renderer>().materials = mats;

            cursor = GameObject.Find("Cursor").GetComponent<CursorUI>();
        }
    }

    private void OnMouseDrag()
    {
        if (isMoveable)
        {
            mousePositionWorld = Input.mousePosition;
            
            mousePositionWorld.z = cubeZ;
            mousePositionWorld = cam.ScreenToWorldPoint(mousePositionWorld);

            float distanceFromCenter = Mathf.Sqrt((Mathf.Pow(mousePositionWorld.x, 2) - 0) + (Mathf.Pow(mousePositionWorld.z, 2) - 0));
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //Physics.Raycast(ray, out hit, 100, layerMask);

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

    private void OnMouseEnter()
    {
        if (isMoveable)
        {
            cursor.OnCursorEnter();
        }
    }

    private void OnMouseExit()
    {
        if (isMoveable)
        {
            cursor.OnCursorExit();
        }
    }
}
