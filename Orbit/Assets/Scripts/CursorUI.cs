using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CursorUI : MonoBehaviour
{
    Vector2 view;
    bool isOutside;
    
    Animator anim;
    RectTransform rTrans;

    private void Start()
    {
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        rTrans = GetComponent<RectTransform>();
    }
    private void Update()
    {
        view = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;

        if (!isOutside)
        {
            rTrans.position = Input.mousePosition;
            Cursor.visible = false;
        }

        if(Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isDown", true);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isDown", false);
        }
    }

    public void OnCursorEnter()
    {
        anim.SetBool("isHovering", true);
    }

    public void OnCursorExit()
    {
        anim.SetBool("isHovering", false);
    }
}
