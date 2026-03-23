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
    [SerializeField] float moveSpeed = 10;
    [SerializeField] static bool isLocked;

    private Vector3 mousePositionWorld;

    private void Awake()
    {
        isLocked = false;
    }

    private void Update()
    {
        // Lock sun
        if (Input.GetKeyDown(KeyCode.Space)) { isLocked = !isLocked; }
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
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            mousePositionWorld.z = 42.1604f;
        }
        else
        {
            mousePositionWorld.z = 46.36f;
        }

        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionWorld);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(mousePositionWorld - transform.position, Vector3.up), Time.deltaTime * moveSpeed);
    }
}
