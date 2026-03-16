using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSun : MonoBehaviour
{
    [SerializeField] GameObject sun;
    [SerializeField] float moveSpeed = 100000;

    Camera mainCam;

    private Vector3 mousePositionWorld;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            RotateSun(true);
        }
    }

    private void OnMouseDrag()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            RotateSun(false);
        }
    }

    bool HoverTest()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (hit.transform.name == "SunTrackHolder")
            {
                //Debug.Log("hovering");
                return true;
            }
        }
        return false;
    }

    void RotateSun(bool isTitle)
    {
        mousePositionWorld = Input.mousePosition;
        if (isTitle){ mousePositionWorld.z = 42.1604f; }
        else { mousePositionWorld.z = 46.36f; }

        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionWorld);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(mousePositionWorld - transform.position, Vector3.up), 1);
    }
}