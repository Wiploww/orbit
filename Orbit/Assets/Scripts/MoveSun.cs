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
    [SerializeField] CinemachineVirtualCamera freeCam;
    [SerializeField] CinemachineVirtualCamera sunCam;
    [SerializeField] Texture2D[] cursorSprite;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float mouseScrollY;

    Camera mainCam;

    //CinemachineVirtualCamera activeCam;
    //DefaultActions actions;
    Rigidbody rb;
    //bool hover;
    private Vector3 mousePositionWorld;

    private void Awake()
    {
        //actions = new DefaultActions();
        //actions.PlayerControls.MoveSun.performed += ctx => mouseScrollY = ctx.ReadValue<float>();

        rb = GetComponent<Rigidbody>();

        mainCam = Camera.main;
    }

    #region - Enable/Disable Arrow Keys -
    /*
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
    */
    #endregion

    private void Update()
    {
        #region - Scroll -

        if (mouseScrollY > 0)
        {
            rb.angularVelocity = new Vector3(0, (moveSpeed * Time.deltaTime) + moveSpeed, 0);
        }
        else if (mouseScrollY < 0)
        {
            rb.angularVelocity = new Vector3(0, (moveSpeed * Time.deltaTime) - moveSpeed, 0);
        }

        #endregion
    }

    void FixedUpdate()
    {
        #region - Follow Mouse -

        mousePositionWorld = Input.mousePosition;
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            mousePositionWorld.z = 42.1604f;
        }
        else
        {
            mousePositionWorld.z = 46.36f;
        }

        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionWorld);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(mousePositionWorld - transform.position, Vector3.up), Time.deltaTime * moveSpeed);

        #endregion


        #region - Arrow Keys & AD -
        /*
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, moveSpeed, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -moveSpeed, 0);
        }
        */
        #endregion

        #region - Drag Controls -
        /*
        if (Input.GetMouseButtonDown(0))
        {
            hover = HoverTest();
        }

        if (Input.GetMouseButton(0) && hover)
        {
            //deltaMousePosition = new Vector3(Input.mousePosition.x - lastMousePosition.x, Input.mousePosition.y - lastMousePosition.y);
            //lastMousePosition = Input.mousePosition;

            DragSun();
        }
        */
        #endregion
    }

    /*
    #region - Click & Drag -
    void DragSun()
    {

    }

    bool HoverTest()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (hit.transform.name == "SunTrackHolder")
            {
                return true;
            }
        }
        return false;
    }
    
    #endregion
    
    void MoveSunFree()
    {
        Debug.Log("Moving Sun");
    }

    void MoveSunLocked()
    {

    }
    */
}
