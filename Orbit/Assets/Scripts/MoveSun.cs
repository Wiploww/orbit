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
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject sun;
    [SerializeField] float moveSpeed = 10;

    Camera mainCam;

    private Vector3 mousePositionWorld;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        #region - Drag Controls -

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            RotateSun(true);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                RotateSun(false);
            }
        }
        
        #endregion
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
        Debug.Log(mousePositionWorld);
        if (isTitle) { mousePositionWorld.z = 42.1604f; }
        else { mousePositionWorld.z = 46.36f; }

        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionWorld);
        transform.rotation = Quaternion.LookRotation(mousePositionWorld);

        cursor.transform.position = mousePositionWorld;
    }
}
