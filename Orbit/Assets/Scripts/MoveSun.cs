using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class MoveSun : MonoBehaviour
{
    [SerializeField] GameObject sun;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] static bool isLocked;
    static bool rightLock = false;

    private Vector3 mousePositionWorld;
    float sunZ;

    private void Awake()
    {
        isLocked = false;

        if (SceneManager.GetActiveScene().name == "MainMenu") { sunZ = 11.5f; }
        else { sunZ = 46.36f; }
    }

    private void Update()
    {
        // Lock sun
        if (Input.GetMouseButtonDown(0)) { isLocked = true; }
        if (Input.GetMouseButtonDown(1)) 
        { 
            isLocked = !isLocked; 
            rightLock = !rightLock;
        }
        if (Input.GetMouseButtonUp(0) && !rightLock) { isLocked = false; }
    }

    void FixedUpdate()
    {
        if (!isLocked)
        {
            FollowMouse();
        }
    }

    void FollowMouse()
    {
        mousePositionWorld = Input.mousePosition;
        mousePositionWorld.z = sunZ;

        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionWorld);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(mousePositionWorld - transform.position, Vector3.up), Time.deltaTime * moveSpeed);
    }
}
