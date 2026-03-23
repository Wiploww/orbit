using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] bool isMoveable;
    [SerializeField] GameObject spaceSlider;
    
    Vector3 mousePositionWorld;

    private void Start()
    {
        if (isMoveable) { spaceSlider.SetActive(true); }
    }

    private void OnMouseDrag()
    {
        if (isMoveable)
        {
            Debug.Log(mousePositionWorld);
            mousePositionWorld = Input.mousePosition;
            
            mousePositionWorld.z = 49f;
            mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionWorld);

            float distanceFromCenter = Mathf.Sqrt((Mathf.Pow(mousePositionWorld.x, 2) - 0) + (Mathf.Pow(mousePositionWorld.z, 2) - 0));

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
