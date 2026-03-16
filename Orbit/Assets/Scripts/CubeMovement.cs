using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] GameObject spaceSlider;
    Vector3 mousePositionWorld;

    private void Start()
    {
        spaceSlider.SetActive(true);
    }

    private void OnMouseDrag()
    {
        mousePositionWorld = Input.mousePosition;
        mousePositionWorld.z = 49f;

        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionWorld);

        transform.position = mousePositionWorld;
    }
    
}
