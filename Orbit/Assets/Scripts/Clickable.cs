using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    static CursorUI cursor;

    GameObject cursorSearch;
    public bool isInteractable = true;

    public void BaseStart()
    {
        cursorSearch = GameObject.Find("Cursor");

        if (cursorSearch != null)
        {
            cursor = cursorSearch.GetComponent<CursorUI>();
        }
    }

    private void OnMouseEnter()
    {
        if (isInteractable)
        {
            cursor.OnCursorEnter();
        }
    }

    private void OnMouseExit()
    {
        if (isInteractable)
        {
            cursor.OnCursorExit();
        }
    }
}
